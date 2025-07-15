
using System;
using System.Collections.Generic;
using Code.Scripts.Components.Card.ScriptableObjects;
using Code.Scripts.Components.GameManagment;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.Components.GameBoard
{
    public class GameBoard : MonoBehaviour
    {
        public Stack<ACard> DiscardStack;
        public Stack<ACard> DrawStack;
        
        public List<ACard> PlayerHand;
        
        public GameObject cardPrefab;
        
        public CardSO[] CurrentFullDeck;
        
        public GameBoard()
        {
            DiscardStack = new Stack<ACard>();
            DrawStack = new Stack<ACard>();
            PlayerHand = new List<ACard>();
        }
        
        public void Initialize(GameManager gameManager)
        {
            DiscardStack.Clear();
            DrawStack.Clear();
            PlayerHand.Clear();
            InitializeCards();
        }
        
        public void AddToDrawStack(IEnumerable<ACard> cards)
        {
            foreach (var card in cards)
            {
                DrawStack.Push(card);
            }
        }
        
        public void AddToDiscardStack(ACard card)
        {
            DiscardStack.Push(card);
        }
        
        public void AddToDrawStack(ACard card)
        {
            DrawStack.Push(card);
        }
        
        public void AddToPlayerHand(ACard card)
        {
            PlayerHand.Add(card);
        }
        
        public void RemoveFromPlayerHand(ACard card)
        {
            PlayerHand.Remove(card);
        }
        
        public void ClearDiscardStack()
        {
            DiscardStack.Clear();
        }
        
        public void RefillPlayerHand(Action onComplete = null)
        {
            var seq = DOTween.Sequence();

   			int cardsToDraw = GameManager.Instance.Player.HandDeck.MaxCardsInHand - PlayerHand.Count;

            Debug.Log($"Refilling player hand with {cardsToDraw} cards. Current hand count: {PlayerHand.Count}");
    		for (int i = 0; i < cardsToDraw; i++)
    		{
     		    seq.AppendCallback(() => AddCardToHand());
        		seq.AppendInterval(0.3f);
   			}

			seq.OnComplete(() => onComplete?.Invoke());
        }

        private void InitializeCards()
        {
            Debug.Log($"{CurrentFullDeck.Length} cards in the current full deck. Initializing draw stack... {CurrentFullDeck}");
            foreach (CardSO card in CurrentFullDeck)
            {
                ACard cardComponent = CreateCardInstance(card);
                DrawStack.Push(cardComponent);
            }
        }
        
        private ACard CreateCardInstance(CardSO cardSo)
        {
            GameObject cardObject = Instantiate(cardPrefab, transform);
            ACard cardComponent = cardObject.GetComponent<ACard>();
            cardComponent.SetCardData(cardSo);
            return cardComponent;
        }
		
        
        public void AddCardToHand()
        {
            ACard drawnCard = DrawStack.Count > 0 ? DrawStack.Pop() : RefillAndGiveCard();
            if (drawnCard != null)
            {
                PlayerHand.Add(drawnCard);
                GameManager.Instance.Player.HandDeck.AddCard(drawnCard);
            }
        }
        
        private ACard RefillAndGiveCard()
        {
            if (DiscardStack.Count == 0) return null;
            
            // Refill the draw stack from the discard stack
            var discardCards = new List<ACard>(DiscardStack);
            DiscardStack.Clear();
            foreach (var card in discardCards)
            {
                DrawStack.Push(card);
            }
            
            // Shuffle the draw stack
            var shuffledCards = new List<ACard>(DrawStack);
            for (int i = 0; i < shuffledCards.Count; i++)
            {
                int j = Random.Range(i, shuffledCards.Count);
                (shuffledCards[i], shuffledCards[j]) = (shuffledCards[j], shuffledCards[i]);
            }
            
            // Return the top card from the now shuffled draw stack
            return DrawStack.Count > 0 ? DrawStack.Pop() : null;
        }
    }
}