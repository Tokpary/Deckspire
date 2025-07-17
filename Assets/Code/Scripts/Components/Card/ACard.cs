using System;
using System.Collections.Generic;
using Code.Scripts.Components.Card.ScriptableObjects;
using Code.Scripts.Components.GameManagment;
using Code.Scripts.Components.Handdeck;
using Code.Scripts.Components.Interfaces;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ACard : MonoBehaviour, ICard, IPointerClickHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private CardSO _cardData;
    private Image _cardImage;
    private TMP_Text _cardNameText;
    private TMP_Text _cardDescriptionText;
    private TMP_Text _cardLifeTimeText;
    private TMP_Text _cardEnergyCostText;
    private Image _cardBackgroundImage;

    private Vector3 _originalPosition;
	public int EnergyCost { get; set; }
	public int LifeTime { get; set; }
    private bool _cardDeployedOnTable;
    private bool _readyToUse;
    
    private bool _isSelected = false;
    [SerializeField] private Transform childTransform;

    private void Awake()
    {
        _cardImage = childTransform.Find("CardSprite").GetComponent<Image>();
        _cardBackgroundImage = childTransform.GetComponent<Image>();
        _cardNameText = childTransform.Find("CardTitle").GetComponent<TMP_Text>();
        _cardDescriptionText = childTransform.Find("CardDescription").GetComponent<TMP_Text>();
        _cardLifeTimeText = childTransform.Find("LifeTimeIndicator").GetComponent<TMP_Text>();
        _cardEnergyCostText = childTransform.Find("EnergyIndicator").GetComponent<TMP_Text>();

        if (_cardData != null)
        {
            InitializeCard();
        }
    }

    public void SetCardDeployed(bool deployedOnTable)
    {
        _cardDeployedOnTable = deployedOnTable;
        _isSelected = false;
    }
    
    public void InitializeCard()
    {
        _cardImage.sprite = _cardData.cardImage;
        _cardBackgroundImage.sprite = _cardData.cardBackground;
        _cardNameText.text = _cardData.cardName;
        _cardDescriptionText.text = _cardData.description;
        _cardLifeTimeText.text = $"{_cardData.lifetime}";
        _cardEnergyCostText.text = $"{_cardData.manaCost}";
		this.EnergyCost = _cardData.manaCost;
		this.LifeTime = _cardData.lifetime;
        _readyToUse = false;
    }

    public bool GetReadyToUse()
    {
        return _readyToUse;
    }

    public void ResetValues()
    {
        this.EnergyCost = _cardData.manaCost;
        this.LifeTime = _cardData.lifetime;
        _cardDeployedOnTable = false;
        _readyToUse = false;
        _isSelected = false;
        UpdateCard();
    }

    public void SetReadyToUse(bool value)
    {
        _readyToUse = value;
    }
    
    public virtual void PlayCard() {
        foreach (var ability in _cardData.abilities) {
            ability.Activate(this);
            
        }
    }

	public void UpdateCard(){
        _cardLifeTimeText.text = $"{this.LifeTime}";
        _cardEnergyCostText.text = $"{this.EnergyCost}";
	}

    public void Drop()
    {
        throw new NotImplementedException();
    }

    public void Select()
    {
        _isSelected = true;
    }
    
    public void Deselect()
    {
        _isSelected = false;
        transform.DOScale(0.2f, 0.1f);
    }
    
    public void SetCardData(CardSO cardData)
    {
        _cardData = cardData;
        InitializeCard();
    }
    
    public CardSO GetDataCard()
    {
        return _cardData;
    }
    
	public void Discard(){
	}
    
    public void SetSortingOrder(int order)
    {
        foreach (var r in GetComponentsInChildren<Renderer>())
            r.sortingOrder = order;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_isSelected)
        {
            Plane dragPlane = new Plane(Vector3.up, new Vector3(0, 1.81f, 0)); // Plano horizontal a nivel y=0 (ajusta si la mesa está más alta)
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (dragPlane.Raycast(ray, out float distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                transform.position = hitPoint;

                transform.localScale = new Vector3(0.25f, 0.25f, 1f); // opcional: visual feedback
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_cardDeployedOnTable && _readyToUse)
        {
            GameManager.Instance.GameBoard.UseCardFromAbilityMat(this);
            _cardDeployedOnTable = false;
            _readyToUse = false;
            return;
        }
        
        if(_cardDeployedOnTable) return;
        Debug.Log($"{_cardDeployedOnTable} - {_readyToUse} - {_isSelected} - Card clicked: {gameObject.name}");
        if(_isSelected) return;
        GameManager.Instance.Player.HandDeck.SelectCard(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_cardDeployedOnTable && !_readyToUse) return;
        // Change the card's appearance to indicate selection
        transform.DOScale(0.25f, 0.15f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Change the card's appearance to indicate deselection
        transform.DOScale(0.2f, 0.1f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.TryGetComponent<ISnapZone>(out var snapZone) && snapZone.CanAcceptCard(this))
            {
                snapZone.SnapCard(this);
                return;
            }
        }
        transform.DOMove(_originalPosition, 0.35f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                transform.DOShakePosition(0.35f, 0.1f, 10, 90, false, true)
                    .OnComplete(() => { 
                            transform.DOScale(0.2f, 0.1f);
                }); 
            });
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalPosition = transform.position;
    }
}