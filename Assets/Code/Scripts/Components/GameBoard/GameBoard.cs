
using System;
using System.Collections.Generic;
using Code.Scripts.Components.Camera;
using Code.Scripts.Components.Card.ScriptableObjects;
using Code.Scripts.Components.Entity;
using Code.Scripts.Components.GameBoard.SnappableArea;
using Code.Scripts.Components.GameManagment;
using Code.Scripts.Components.GameManagment.GameStates;
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
        public TappableSnapArea ExtraSlot;
        
        private int RefillTimes = 0;
        public GameBoard()
        {
            DiscardStack = new Stack<ACard>();
            DrawStack = new Stack<ACard>();
            PlayerHand = new List<ACard>();
            AbilityMat = new List<ACard>();
            RulesMat = new List<ACard>();
        }
        
        public void Initialize(GameManager gameManager, Enemy enemy)
        {
            DiscardStack.Clear();
            DrawStack.Clear();
            PlayerHand.Clear();
            GameRulesData = GetComponent<GameRulesData>();
            GameRulesData.UpdateEnemyRules(enemy.EnemyRulesData);
            InitializeCards();
            InitializePlayer();
        }

        private void InitializePlayer()
        {
            GameManager.Instance.Player.Initialize(GameRulesData);
        }
        
        public Sequence RemoveFromPlayerHandTween(ACard card, Action onComplete = null)
        {
            Sequence s = DOTween.Sequence();

            DiscardStack.Push(card);
            card.CardStatus = CardStatus.Discarded;
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
            if(GameManager.Instance.GameBoard.GameRulesData.DrawOnEmptyHandOnly && PlayerHand.Count > 0)
            {
                GameManager.Instance.Player.HandDeck.DeployCardsInHand();
                foreach (var card in AbilityMat)
                {
                    if (card.CardStatus == CardStatus.DeployedOnAbilitiesInactive)
                    {
                        seq.AppendCallback(() => {
                            card.CardStatus = CardStatus.DeployedOnAbilitiesActive;
                            card.transform.DORotate(new Vector3(90, 0, 0), 0.3f);  });
                        seq.AppendInterval(0.3f);
                    }
                }
                seq.OnComplete(() => onComplete?.Invoke());
                return;
            }
            

   			int cardsToDraw = GameRulesData.MaxHandSize - PlayerHand.Count;

    		for (int i = 0; i < cardsToDraw; i++)
    		{
     		    seq.AppendCallback(() => AddCardToHand());
        		seq.AppendInterval(0.3f);
   			}

            foreach (var card in AbilityMat)
            {
                if (card.CardStatus == CardStatus.DeployedOnAbilitiesInactive)
                {
                    seq.AppendCallback(() => {
                        card.CardStatus = CardStatus.DeployedOnAbilitiesActive;
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
                cardComponent.CardStatus = CardStatus.Discarded; // Set initial status
            }
            ShuffleDrawStack();
        }

        public void FriendlyFireEnabled()
        {
            foreach (CardSO card in CurrentFullDeck)
            {
                if (card.isDamageAbility)
                {
                    card.isCardModifier = true;
                }
            }
        }

        public void FriendlyFireDisabled()
        {
            foreach (CardSO card in CurrentFullDeck)
            {
                if (card.isDamageAbility)
                {
                    card.isCardModifier = false;
                }
            }
        }

        
        public void DisplayCardInTable(ACard card)
        {
            GameManager.Instance.UIManager.UpdateEnergy(GameManager.Instance.Player.CurrentEnergy);
            card.Deselect();
            PlayerHand.Remove(card); 
            switch (card.GetDataCard().cardType)
            {
                case 0:
                    DiscardStack.Push(card);
                    card.CardStatus = CardStatus.Discarded;
                    break;
                case 1:
                    AbilityMat.Add(card);
                    card.CardStatus = CardStatus.DeployedOnAbilitiesInactive;
                    break;
                case 2:
                    RulesMat.Add(card);
                    card.CardStatus = CardStatus.DeployedOnRules;
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
            }

            if (RulesMat.Contains(card))
            {
                RulesMat.Remove(card);
            }

            if (AbilityMat.Contains(card))
            {
                AbilityMat.Remove(card);
            }
            
            DiscardStack.Push(card);
            card.CardStatus = CardStatus.Discarded;
        }
        
        private ACard CreateCardInstance(CardSO cardSo)
        {
            GameObject cardObject = Instantiate(cardPrefab, transform);
            ACard cardComponent = cardObject.GetComponent<ACard>();
            cardComponent.CardStatus = CardStatus.Discarded;
            cardComponent.SetCardData(cardSo);
            return cardComponent;
        }

		
        
        public void AddCardToHand()
        {
            ACard drawnCard = DrawStack.Count > 0 ? DrawStack.Pop() : RefillAndGiveCard();
            if (drawnCard != null)
            {
                PlayerHand.Add(drawnCard);
                drawnCard.CardStatus = CardStatus.InHand;
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
                card.CardStatus = CardStatus.InDeck; // Reset the card status
            }
            
            ShuffleDrawStack();
            RefillTimes++;
            if(RefillTimes >= GameRulesData.NumberOfRefillsToWin && GameRulesData.NumberOfRefillsToWin > 0)
            {
                Debug.Log("Game Over: Refill limit reached.");
                //GameManager.Instance.GameFlowManager.SetState(new DialogueState(GameManager.Instance.GameFlowManager, "StartGame"));
                return null;
            }
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
                card.CardStatus = CardStatus.InDeck; // Reset the card status
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
                                    card.CardStatus = CardStatus.Discarded;
                                    onComplete?.Invoke();
                                });
                            });
                    });
                });
           
        }

        public void DestroyCard(ACard card)
        {
            PlayerHand.Remove(card);
            Destroy(card.gameObject);
            GameManager.Instance.Player.HandDeck.DeployCardsInHand();
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