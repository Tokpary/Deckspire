
using System.Collections.Generic;
using Code.Scripts.Components.Card.ScriptableObjects;
using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.GameBoard
{
    public class GameBoard 
    {
        public Stack<CardSO> DiscardStack;
        public Stack<CardSO> DrawStack;
        
        public List<CardSO> PlayerHand;
        
        public GameObject cardPrefab;
        
        public GameBoard()
        {
            DiscardStack = new Stack<CardSO>();
            DrawStack = new Stack<CardSO>();
            PlayerHand = new List<CardSO>();
        }
        
        public void Initialize(GameManager gameManager)
        {
            DiscardStack.Clear();
            DrawStack.Clear();
            PlayerHand.Clear();
            DrawStack = new Stack<CardSO>(gameManager.Player.CurrentFullDeck);
        }
        
        public void AddToDrawStack(IEnumerable<CardSO> cards)
        {
            foreach (var card in cards)
            {
                DrawStack.Push(card);
            }
        }
        
        public void AddToDiscardStack(CardSO card)
        {
            DiscardStack.Push(card);
        }
        
        public void AddToDrawStack(CardSO card)
        {
            DrawStack.Push(card);
        }
        
        public void AddToPlayerHand(CardSO card)
        {
            PlayerHand.Add(card);
        }
        
        public void RemoveFromPlayerHand(CardSO card)
        {
            PlayerHand.Remove(card);
        }
        
        public void ClearDiscardStack()
        {
            DiscardStack.Clear();
        }
        
        public void RefillPlayerHand()
        {
            Debug.Log("Refilling Player Hand");
            Debug.Log($"Current Hand Count: {PlayerHand.Count}");
            Debug.Log($"Draw Stack Count: {DrawStack.Count}");
            Debug.Log($"Max Cards in Hand: {GameManager.Instance.Player.HandDeck.MaxCardsInHand}");
            while (PlayerHand.Count < GameManager.Instance.Player.HandDeck.MaxCardsInHand)
            {
                AddCardToHand();
            }
        }
        
        public void AddCardToHand()
        {
            CardSO drawnCard = DrawStack.Count > 0 ? DrawStack.Pop() : RefillAndGiveCard();
            
            if (drawnCard != null)
            {
                PlayerHand.Add(drawnCard);
                GameManager.Instance.Player.HandDeck.AddCard(drawnCard);
            }
        }
        
        private CardSO RefillAndGiveCard()
        {
            if (DiscardStack.Count == 0) return null;
            
            // Refill the draw stack from the discard stack
            var discardCards = new List<CardSO>(DiscardStack);
            DiscardStack.Clear();
            foreach (var card in discardCards)
            {
                DrawStack.Push(card);
            }
            
            // Shuffle the draw stack
            var shuffledCards = new List<CardSO>(DrawStack);
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