using System.Collections.Generic;
using Code.Scripts.Components.Card.ScriptableObjects;
using Code.Scripts.Components.Interfaces;
using Code.Scripts.DesignPatterns;
using DG.Tweening;
using UnityEngine;

namespace Code.Scripts.Components.Handdeck
{
    public class HandDeckManager : MonoBehaviour
    {
        [SerializeField] List<ACard> handCards = new List<ACard>();
        [SerializeField] Transform _cameraTransform;
        
        [SerializeField] public float cardScale = 0.5f; // Scale of the cards in hand
        [SerializeField] public float cardSpace = 2f; // Space between cards in hand
        [SerializeField] public float cardHeight = 0.4f; // Height of the cards above the camera
        [SerializeField] public float cardDistanceFromCamera = 0.5f; // Scale of the cards
        [SerializeField] public float arcAngle = 0.015f; // Scale of the cards
        
        [SerializeField] private int _maxCardsInHand = 4; // Maximum number of cards in hand

        public GameObject cardPrefab; // Prefab for the cards in hand
        public int MaxCardsInHand { get; set; }
        public event System.Action<ACard> OnCardSelected;
        public event System.Action<ACard> OnCardDeselected;
        
        private ACard selectedCard;
        
        public void SelectCard(ACard card)
        {
            if (selectedCard != null)
            {
                selectedCard.Deselect();
            }

            if (card == selectedCard)
            {
                selectedCard.Deselect();
                selectedCard = null;
                DeployCardsInHand();
                OnCardDeselected?.Invoke(card);
                return;
            }
            
            selectedCard = card;
            selectedCard.Select();
            OnCardSelected?.Invoke(card);
            
            DeployCardsInHand();
        }

        public int GetCurrentCardCount()
        {
            return handCards.Count;
        }
        
        void Awake()
        {
            MaxCardsInHand = _maxCardsInHand; 
        }
        
        public void AddCard(ACard card, bool deploy = true)
        {
            if (card == null) return;
            
            
            handCards.Add(card);
            
            if(deploy)
                DeployCardsInHand();
        }

        [ContextMenu("Deploy Cards in Hand")]
        public void DeployCardsInHand()
        {
            if (handCards.Count == 0) return;

            float half = (handCards.Count - 1) / 2f;
            float zOffsetPerCard = 0.001f;
            
                for (int i = 0; i < handCards.Count; i++)
                {
                    ACard card = handCards[i];

                    float t = half == 0 ? 0 : (i - half) / half; // Evita NaN
                    float angle = t * arcAngle * cardSpace;

                    // 2. Posición en arco (rotando la dirección de la cámara hacia los lados)
                    Quaternion yaw = Quaternion.AngleAxis(-angle, Vector3.up);
                    Vector3 dir = yaw * _cameraTransform.forward;

                    // 2.5 Añadir leve desplazamiento en profundidad para mejorar el orden de interacción
                    float depthOffset = (i) * zOffsetPerCard; // Más arriba = más cerca
                    Vector3 pos = _cameraTransform.position + dir * (cardDistanceFromCamera + depthOffset)
                                                            + _cameraTransform.up * cardHeight;

                    // 3. Rotación: que la carta mire a la cámara
                    angle = Mathf.Repeat(angle, 360f);

                    Vector3 toCam = _cameraTransform.position - pos;
                    if (toCam.sqrMagnitude < 0.001f)
                        toCam = _cameraTransform.forward;

                    Quaternion look = Quaternion.LookRotation(toCam.normalized, _cameraTransform.up);
                    look *= Quaternion.Euler(0, 180, angle);

// Aplicar rotación en espacio global
                    card.transform.DOMove(pos, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                    {
                        card.transform.DORotate(look.eulerAngles, 0.5f).SetEase(Ease.OutBack);
                    });
                    card.transform.localScale = Vector3.one * cardScale;
                
                    handCards[i].SetSortingOrder(handCards.Count - i - 1);
                }
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
