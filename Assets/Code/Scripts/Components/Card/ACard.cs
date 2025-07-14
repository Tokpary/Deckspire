using System;
using Code.Scripts.Components.Handdeck;
using Code.Scripts.ScriptableObjects;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ACard : MonoBehaviour, ICard, IPointerClickHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CardSO _cardData;
    private Image _cardImage;
    private TMP_Text _cardNameText;
    private TMP_Text _cardDescriptionText;
    private TMP_Text _cardLifeTimeText;
    private TMP_Text _cardEnergyCostText;

	public int EnergyCost { get; set; }
	public int LifeTime { get; set; }
    
    private bool _isSelected = false;
    [SerializeField] private Transform childTransform;

    private void Awake()
    {
        _cardImage = childTransform.Find("CardSprite").GetComponent<Image>();
        _cardNameText = childTransform.Find("CardTitle").GetComponent<TMP_Text>();
        _cardDescriptionText = childTransform.Find("CardDescription").GetComponent<TMP_Text>();
        _cardLifeTimeText = childTransform.Find("LifeTimeIndicator").GetComponent<TMP_Text>();
        _cardEnergyCostText = childTransform.Find("EnergyIndicator").GetComponent<TMP_Text>();

        if (_cardData != null)
        {
            InitializeCard();
        }
    }
    
    public void InitializeCard()
    {
        _cardImage.sprite = _cardData.cardImage;
        _cardNameText.text = _cardData.cardName;
        _cardDescriptionText.text = _cardData.description;
        _cardLifeTimeText.text = $"{_cardData.lifetime}";
        _cardEnergyCostText.text = $"{_cardData.manaCost}";
		this.EnergyCost = _cardData.manaCost;
		this.LifeTime = _cardData.lifetime;
    }
    
    public virtual void PlayCard(TriggerTiming timing, GameContext context) {
        foreach (var ability in cardData.abilities) {
            if (ability.triggerTiming == timing)
            {
                ability.Activate(this, context);
            }
        }
    }

	public void UpdateCard(){
        _cardLifeTimeText.text = $"{this.LifeTime}";
        _cardEnergyCostText.text = $"{this.EnergyCost}";
	}
    
    public void Select()
    {
        _isSelected = true;
    }
    
    public void Deselect()
    {
        _isSelected = false;
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
            if (eventData.delta.y > 0)
            {
                Debug.Log("Card deployed on table");
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HandDeckManager.Instance.SelectCard(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change the card's appearance to indicate selection
        transform.DOScale(0.25f, 0.15f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Change the card's appearance to indicate deselection
        transform.DOScale(0.2f, 0.1f);
    }
}