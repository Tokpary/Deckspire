
using System;
using System.Collections.Generic;
using Code.Scripts.Components.Camera;
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
        
        public GameRulesData GameRulesData { get; set; }
        public GameObject cardPrefab;
        
        public Transform DiscardStackTransform;
        
        public CardSO[] CurrentFullDeck;
        
        public Transform NewCardsTransform;
        
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
            GameRulesData = GetComponent<GameRulesData>();
            InitializeCards();
            InitializePlayer();
        }

        private void InitializePlayer()
        {
            GameManager.Instance.Player.Initialize(GameRulesData);
        }
        
        public void AddToDrawStack(IEnumerable<ACard> cards)
        {
            foreach (var card in cards)
            {
                DrawStack.Push(card);
            }
        }
        
        
        public void AddToDrawStack(ACard card)
        {
            DrawStack.Push(card);
        }
        
        public void AddToPlayerHand(ACard card)
        {
            PlayerHand.Add(card);
        }
        
        public Sequence RemoveFromPlayerHandTween(ACard card, Action onComplete = null)
        {
            Sequence s = DOTween.Sequence();

            DiscardStack.Push(card);
            PlayerHand.Remove(card);
            
            s.Append(card.transform.DOMove(DiscardStackTransform.position, 0.5f).SetEase(Ease.InOutQuad));
            s.Append(card.transform.DORotate(new Vector3(90, 0, 0), 0.2f).SetEase(Ease.InOutQuad));
            
            return s;
        }
        
        public void ClearDiscardStack()
        {
            DiscardStack.Clear();
        }
        
        public void RefillPlayerHand(Action onComplete = null)
        {
            var seq = DOTween.Sequence();

   			int cardsToDraw = GameRulesData.MaxHandSize - PlayerHand.Count;

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
                        card.transform.DORotate(new Vector3(90, 0, 0), 0.3f);  });
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
            GameManager.Instance.UIManager.UpdateEnergy(GameManager.Instance.Player.CurrentEnergy);
            card.Deselect();
            PlayerHand.Remove(card); 
            card.SetCardDeployed(true);
            switch (card.GetDataCard().cardType)
            {
                case 0:
                    DiscardStack.Push(card);
                    card.SetReadyToUse(false);
                    break;
                case 1:
                    AbilityMat.Add(card);
                    break;
                case 2:
                    RulesMat.Add(card);
                    break;
            }
            CameraManager.Instance.ReturnToTableView();
        }

        public void UseCardFromAbilityMat(ACard card)
        {
            AbilityMat.Remove(card);
            card.PlayCard();
            MoveCardToDiscard(card);
            CameraManager.Instance.ReturnToTableView();
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
                drawnCard.ResetValues();
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

        public void AddCardToDiscardStackFromEnemy(CardSO cardSo, Action onComplete = null)
        {
            ACard card = CreateCardInstance(cardSo);
            card.transform.position = NewCardsTransform.position;
            card.transform.rotation = Quaternion.Euler(0, 0, 0);
            
            // Se mueve en frente de la camara
            card.transform.DOMove(CameraManager.Instance.transform.position + CameraManager.Instance.transform.forward * 0.35f, 0.5f)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() => 
                {
                    card.transform.DORotate(new Vector3(0, 0, 0), 2f).SetEase(Ease.InOutQuad).OnComplete(() =>
                    {
                        card.transform.DOMove(DiscardStackTransform.position, 0.5f)
                            .SetEase(Ease.InOutQuad)
                            .OnComplete(() =>
                            {
                                card.transform.DORotate(new Vector3(90, 0, 0), 0.2f).SetEase(Ease.InOutQuad).OnComplete(() =>
                                {
                                    DiscardStack.Push(card);
                                    card.Deselect();
                                    onComplete?.Invoke();
                                });
                            });
                    });
                });
           
        }

        public void MoveCardToDiscard(ACard card)
        {
            card.transform.DOMove(DiscardStackTransform.position, 0.5f)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    card.transform.DORotate(new Vector3(90, 0, 0), 0.2f).SetEase(Ease.InOutQuad).OnComplete(() =>
                    {
                        AddToDiscardStack(card);
                    });
                });
        }
    }
}