using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Patterns.State.Interfaces;
using UnityEngine;

namespace Code.Scripts.Components.GameManagment.GameStates
{
    public class AfterEnemyState : AGameState
    {
        public AfterEnemyState(IGameState gameState) : base(gameState)
        {
        }

        public override void Enter(IGameState gameManager)
        {
            Sequence sequence = DOTween.Sequence();

            int discardedCardCant = 0;

            if (!GameManager.Instance.GameBoard.GameRulesData.NextRoundIsFreeze)
            {
                foreach (ACard card in GameManager.Instance.GameBoard.PlayerHand.ToList()) // copiar la lista por seguridad
                {
                    card.LifeTime -= GameManager.Instance.GameBoard.GameRulesData.DecoyPerRound;
                    card.UpdateCard();

                    if (card.LifeTime <= 0)
                    {
                        sequence.Append(GameManager.Instance.GameBoard.RemoveFromPlayerHandTween(card));
                        discardedCardCant++;
                    }
                }
            } 
            else
            {
                GameManager.Instance.GameBoard.GameRulesData.NextRoundIsFreeze = false;
            }
            

            if (GameManager.Instance.GameBoard.GameRulesData.DecoyOnTableCards)
            {
                List<ACard> cardsToRemove = new List<ACard>();
                cardsToRemove.AddRange(GameManager.Instance.GameBoard.AbilityMat);
                cardsToRemove.AddRange(GameManager.Instance.GameBoard.RulesMat);
                
                foreach (ACard card in cardsToRemove)
                {
                    card.LifeTime -= GameManager.Instance.GameBoard.GameRulesData.DecoyPerRound;
                    card.UpdateCard();
                    
                    if (card.LifeTime <= 0)
                    {
                        GameManager.Instance.GameBoard.MoveCardToDiscard(card);
                        card.CurrentSnapZone.RemoveCardFromSlot();
                    }
                }
                
            }
            
            sequence.OnComplete(() =>
            {

                if (GameManager.Instance.GameBoard.GameRulesData.NextRoundIsEnergyLoss)
                {
                    GameManager.Instance.Player.CurrentEnergy = GameManager.Instance.GameBoard.GameRulesData.PlayerMaxMana - 1;
                    GameManager.Instance.GameBoard.GameRulesData.NextRoundIsEnergyLoss = false;
                }
                else
                {
                    GameManager.Instance.Player.CurrentEnergy = GameManager.Instance.GameBoard.GameRulesData.PlayerMaxMana;
                }

                GameManager.Instance.UIManager.UpdateEnergy(GameManager.Instance.Player.CurrentEnergy);
                if (GameManager.Instance.GameBoard.GameRulesData.IsDeathWinCondition && 
                    GameManager.Instance.GameBoard.PlayerHand.Count <= 0)
                {
                        GameManager.Instance.GameFlowManager.SetState(new DialogueState(gameManager, "DeathBeated"));
                        return;
                }
                else
                {
                    
                
                    if (GameManager.Instance.GameBoard.GameRulesData.LifeLossOn2CardsDecoyed)
                    {
                        if (discardedCardCant >= 2)
                        {
                            GameManager.Instance.Player.TakeDamage(1);
                        }
                    }
                    GameManager.Instance.Player.TakeDamage(1);
                    GameManager.Instance.GameFlowManager.SetState(new DrawState(gameManager));
                }
                

            });

            sequence.Play();
        }
        public override void Exit(IGameState gameManager)
        {
           
        }

        public override void Update()
        {
        }
    }
}