
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
        public List<ACard> RulesMat;
        public List<ACard> AbilityMat;
        
        public GameRulesData GameRules;
        public GameObject cardPrefab;
        
        public CardSO[] CurrentFullDeck;
        
        public GameBoard()
        {
            DiscardStack = new Stack<ACard>();
            DrawStack = new Stack<ACard>();
            PlayerHand = new List<ACard>();
            AbilityMat = new List<ACard>();
            RulesMat = new List<ACard>();
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

    		for (int i = 0; i < cardsToDraw; i++)
    		{
     		    seq.AppendCallback(() => AddCardToHand());
        		seq.AppendInterval(0.3f);
   			}

            foreach (var card in AbilityMat)
            {
                if (!card.GetReadyToUse())
                {
                    seq.AppendCallback(() => {
                        card.SetReadyToUse(true);
                        card.transform.DORotate(new Vector3(0, 0, 0), 0.3f);  });
                    seq.AppendInterval(0.3f);
                }
            }

			seq.OnComplete(() => onComplete?.Invoke());
        }

        private void InitializeCards()
        {
            foreach (CardSO card in CurrentFullDeck)
            {
                ACard cardComponent = CreateCardInstance(card);
                DrawStack.Push(cardComponent);
            }
            ShuffleDrawStack();
        }

        public void DisplayCardInTable(ACard card)
        {
            PlayerHand.Remove(card);
            switch (card.GetDataCard().cardType)
            {
                case 0:
                    DiscardStack.Push(card);
                    break;
                case 1:
                    AbilityMat.Add(card);
                    break;
                case 2:
                    RulesMat.Add(card);
                    break;
            }
        }

        public void UseCardFromAbilityMat(ACard card)
        {
            AbilityMat.Remove(card);
            card.PlayCard();
            DiscardStack.Push(card);
        }

        public void AddToDiscardStack(ACard card)
        {
            if (PlayerHand.Contains(card))
            {
                PlayerHand.Remove(card);
                return;
            }

            if (RulesMat.Contains(card))
            {
                RulesMat.Remove(card);
                return;
            }

            if (AbilityMat.Contains(card))
            {
                AbilityMat.Remove(card);
                return;
            }
            
            DiscardStack.Push(card);
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
            
            ShuffleDrawStack();
            
            // Return the top card from the now shuffled draw stack
            return DrawStack.Count > 0 ? DrawStack.Pop() : null;
        }
        
        public void ShuffleDrawStack()
        {
            var shuffledCards = new List<ACard>(DrawStack);
            DrawStack.Clear();
            for (int i = 0; i < shuffledCards.Count; i++)
            {
                int j = Random.Range(i, shuffledCards.Count);
                (shuffledCards[i], shuffledCards[j]) = (shuffledCards[j], shuffledCards[i]);
            }
            foreach (var card in shuffledCards)
            {
                DrawStack.Push(card);
            }
        }
    }
}