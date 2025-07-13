using System;
using Code.Scripts.ScriptableObjects;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ACard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private CardSO _cardData;
    private Image _cardImage;
    private TMP_Text _cardNameText;
    private TMP_Text _cardDescriptionText;
    private TMP_Text _cardLifeTimeText;
    private TMP_Text _cardEnergyCostText;
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
    
    private void InitializeCard()
    {
        _cardImage.sprite = _cardData.cardImage;
        _cardNameText.text = _cardData.cardName;
        _cardDescriptionText.text = _cardData.description;
        _cardLifeTimeText.text = $"{_cardData.lifetime}";
        _cardEnergyCostText.text = $"{_cardData.manaCost}";
    }

    public void OnPointerDown(PointerEventData e)
    {
        transform.DOScale(0.23f, 0.15f);
    }

    public void OnPointerUp(PointerEventData e)
    {
        transform.DOScale(0.2f, 0.1f);
    }
    
    
    public void SetSortingOrder(int order)
    {
        foreach (var r in GetComponentsInChildren<Renderer>())
            r.sortingOrder = order;
    }

    void Update()
    {
    }
}