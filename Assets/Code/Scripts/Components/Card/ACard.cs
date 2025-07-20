using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Components.Card.ScriptableObjects;
using Code.Scripts.Components.GameBoard.SnappableArea;
using Code.Scripts.Components.GameManagment;
using Code.Scripts.Components.GameManagment.GameStates;
using Code.Scripts.Components.Handdeck;
using Code.Scripts.Components.Interfaces;
using DG.Tweening;
using Fungus;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using CameraManager = Code.Scripts.Components.Camera.CameraManager;

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
    private Tween scaleTween;

    private Vector3 _previousMatPosition;
    
    public ASnapZone CurrentSnapZone { get; set; }
    
    public CardStatus CardStatus { get; set; } = CardStatus.Discarded;
    
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

        if (_cardData.isDamageAbility)
        {
            if (GameManager.Instance.GameBoard.GameRulesData.IsFriendlyFireApplied)
            {
                _cardData.isCardModifier = true;
            } 
            else
            {
                _cardData.isCardModifier = false;
            }
        }
    }


    public void ResetValues()
    {
        this.EnergyCost = _cardData.manaCost;
        this.LifeTime = _cardData.lifetime;
        UpdateCard();
    }
    
    public virtual void PlayCard() {
        foreach (var ability in _cardData.abilities) {
            ability.Activate(this);
            
        }
    }

    public void PlayOnExitCard()
    {
        foreach (var ability in _cardData.abilities) 
        {
            ability.Deactivate(this);
        }
    }
    
    public Vector3 GetPreviousMatPosition()
    {
        return _previousMatPosition;
    }

    public void PlayOnSelectedCard(ACard card)
    {
        foreach (var ability in _cardData.abilities) 
        {
            ability.Activate(card, this);
        }
    }

	public void UpdateCard(){
        if (GameManager.Instance.GameBoard.GameRulesData.IsHermitWinCondition)
        {
            if (LifeTime >= 10)
            {
                GameManager.Instance.GameFlowManager.SetState(new DialogueState(GameManager.Instance.GameFlowManager, "LastDialogue"));
            }
        }
        _cardLifeTimeText.text = $"{this.LifeTime}";
        _cardEnergyCostText.text = $"{this.EnergyCost}";
	}

    public void Drop()
    {
        throw new NotImplementedException();
    }

    public void Select()
    {
        CardStatus = CardStatus.Selected;
    }

    public void Deselect()
    {
        CardStatus = CardStatus.InHand;
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
        if (CardStatus == CardStatus.Selected)
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

        switch (CardStatus)
        {
            case CardStatus.Selected:
                break;
            case CardStatus.Discarded:
                break;
            case CardStatus.InDeck:
                break;
            case CardStatus.InHand:
                if (GameManager.Instance.GameBoard.GameRulesData.IsModifyingCard)
                {
                    ACard actionCard = GameManager.Instance.GameBoard.GameRulesData.SelectedCard;
                    if (actionCard.CardStatus == CardStatus.SelectedToModify)
                    {
                        GameManager.Instance.GameBoard.GameRulesData.IsModifyingCard = false;
                        GameManager.Instance.GameBoard.GameRulesData.SelectedCard = null;
                        if(actionCard.EnergyCost > GameManager.Instance.Player.CurrentEnergy)
                        {
                            actionCard.transform.DOShakeRotation(0.3f, 40, 10, 90).OnComplete(() =>
                            {
                                actionCard.CardStatus = CardStatus.InHand;
                                GameManager.Instance.Player.HandDeck.DeselectCard(actionCard);
                            });
                        }
                        else
                        {
                            GameManager.Instance.Player.CurrentEnergy -= actionCard.EnergyCost;
                            GameManager.Instance.UIManager.UpdateEnergy(GameManager.Instance.Player.CurrentEnergy);
                            actionCard.PlayOnSelectedCard(this);
                            GameManager.Instance.GameBoard.MoveCardToDiscard(actionCard);
                        }
                    }
                    else if (actionCard.CardStatus == CardStatus.SelectedToModifyFromMat)
                    {
                        GameManager.Instance.GameBoard.GameRulesData.IsModifyingCard = false;
                        GameManager.Instance.GameBoard.GameRulesData.SelectedCard = null;
                        actionCard.PlayOnSelectedCard(this);
                        if(actionCard.CurrentSnapZone != null)
                            actionCard.CurrentSnapZone.RemoveCardFromSlot();
                        GameManager.Instance.GameBoard.MoveCardToDiscard(actionCard);
                    }
                    
                }
                else
                {
                    GameManager.Instance.Player.HandDeck.SelectCard(this);
                }
                break;
            case CardStatus.SelectedToModify:
                GameManager.Instance.Player.HandDeck.DeselectCard(this);
                break;
            case CardStatus.SelectedToModifyFromMat:
                GameManager.Instance.Player.HandDeck.DeselectCardToMat(this);
                break;
            case CardStatus.DeployedOnRules:
                break;
            case CardStatus.DeployedOnAbilitiesInactive:
                break;
            case CardStatus.DeployedOnAbilitiesActive:
                if(_cardData.isCardModifier)
                {
                    GameManager.Instance.GameBoard.GameRulesData.IsModifyingCard = true;
                    GameManager.Instance.GameBoard.GameRulesData.SelectedCard = this;
                    CardStatus = CardStatus.SelectedToModifyFromMat;
                    _previousMatPosition = transform.position;
                    CameraManager.Instance.ReturnToTableView(() =>
                    {
                        GameManager.Instance.Player.HandDeck.SelectCard(this);
                    });
                }
                else
                {
                    GameManager.Instance.GameBoard.UseCardFromAbilityMat(this);
                    if(CurrentSnapZone != null)
                    {
                        CurrentSnapZone.RemoveCardFromSlot();
                    }
                }
                break;
            
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CardStatus == CardStatus.InHand || CardStatus == CardStatus.DeployedOnAbilitiesActive)
        {
            scaleTween?.Kill(); // Solo mata la animación de escala anterior
            scaleTween = transform.DOScale(0.25f, 0.15f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    
        scaleTween?.Kill(); // Igual aquí, solo matas la animación de escala
        scaleTween = transform.DOScale(0.2f, 0.1f);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (CardStatus != CardStatus.Selected) return;
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
                transform.DOShakeRotation(0.3f, 40, 10, 90)
                    .OnComplete(() => { 
                            transform.DOScale(0.2f, 0.1f);
                }); 
            });
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalPosition = transform.position;
    }

    public void SetLifeTimeVisbility(bool b)
    {
        if (_cardLifeTimeText != null)
        {
            _cardLifeTimeText.gameObject.SetActive(b);
        }
    }
}

public enum CardStatus
{
    InHand,
    DeployedOnRules,
    DeployedOnAbilitiesInactive,
    DeployedOnAbilitiesActive,
    Discarded,
    Selected,
    InDeck,
    SelectedToModify,
    SelectedToModifyFromMat
}