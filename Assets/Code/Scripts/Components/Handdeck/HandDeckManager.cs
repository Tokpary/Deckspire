using System.Collections.Generic;
using Code.Scripts.Components.Card.ScriptableObjects;
using Code.Scripts.Components.GameManagment;
using Code.Scripts.Components.Interfaces;
using Code.Scripts.DesignPatterns;
using DG.Tweening;
using UnityEngine;

namespace Code.Scripts.Components.Handdeck
{
    public class HandDeckManager : MonoBehaviour
    {
        [SerializeField] Transform _cameraTransform;
        
        [SerializeField] public float cardScale = 0.5f; // Scale of the cards in hand
        [SerializeField] public float cardSpace = 2f; // Space between cards in hand
        [SerializeField] public float cardHeight = 0.4f; // Height of the cards above the camera
        [SerializeField] public float cardDistanceFromCamera = 0.5f; // Scale of the cards
        [SerializeField] public float arcAngle = 0.015f; // Scale of the cards
        

        public GameObject cardPrefab; // Prefab for the cards in hand
        public int MaxCardsInHand { get; set; }
        public event System.Action<ACard> OnCardSelected;
        public event System.Action<ACard> OnCardDeselected;
        
        private ACard selectedCard;

        public void DeselectCard(ACard card)
        {
            card.CardStatus = CardStatus.InHand;
            GameManager.Instance.GameBoard.GameRulesData.IsModifyingCard = false;
            GameManager.Instance.GameBoard.GameRulesData.SelectedCard = null;
            GameManager.Instance.GameBoard.PlayerHand.Add(card);
            DeployCardsInHand();
            selectedCard = null;
        }
        
        public void DeselectCardToMat(ACard card)
        {
            card.CardStatus = CardStatus.DeployedOnAbilitiesActive;
            GameManager.Instance.GameBoard.GameRulesData.IsModifyingCard = false;
            GameManager.Instance.GameBoard.GameRulesData.SelectedCard = null;
            GameManager.Instance.GameBoard.AbilityMat.Add(card);
            card.transform.DOMove(card.GetPreviousMatPosition(), 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                card.transform.DORotate(new Vector3(90,0, 0), 0.5f).SetEase(Ease.OutBack);
            });
            DeployCardsInHand();
            selectedCard = null;
        }
        
        public void SelectCard(ACard card)
        {
            if (!card.GetDataCard().isCardModifier || (card.GetDataCard().isCardModifier && card.GetDataCard().cardType == 1 && card.CardStatus != CardStatus.SelectedToModifyFromMat))
            {
                selectedCard = card;
                OnCardSelected?.Invoke(card);
                DeployCardsInHand();
                
            } else
            {
                //display card on middle of screen
                Vector3 screenPos = _cameraTransform.position + _cameraTransform.forward * cardDistanceFromCamera;
                card.transform.DOMove(screenPos + new Vector3(0,0.125f), 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    card.transform.DORotate(_cameraTransform.rotation.eulerAngles, 0.5f).SetEase(Ease.OutBack);
                    if (CardStatus.SelectedToModifyFromMat != card.CardStatus)
                        card.CardStatus = CardStatus.SelectedToModify;
                    GameManager.Instance.GameBoard.GameRulesData.IsModifyingCard = true;
                    GameManager.Instance.GameBoard.GameRulesData.SelectedCard = card;
                    selectedCard = card;
                    DeployCardsInHand();
                    
                });
            }
            
            
        }
        
        public void AddCard(ACard card, bool deploy = true)
        {
            if (card == null) return;
            
            
            if(deploy)
                DeployCardsInHand();
        }

        [ContextMenu("Deploy Cards in Hand")]
        public void DeployCardsInHand()
        {
            List<ACard> handCards = GameManager.Instance.GameBoard.PlayerHand;
            for(int i = handCards.Count - 1; i >= 0; i--)
            {
                if (handCards[i].CardStatus == CardStatus.SelectedToModify)
                {
                    handCards.Remove(handCards[i]);
                }
            }
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
                        card.transform.DORotate(look.eulerAngles, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                        {
                            card.CardStatus = CardStatus.InHand;
                        });
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
