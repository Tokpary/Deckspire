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

            foreach (ACard card in GameManager.Instance.GameBoard.PlayerHand.ToList()) // copiar la lista por seguridad
            {
                card.LifeTime -= GameManager.Instance.GameBoard.GameRulesData.DecoyPerRound;
                card.UpdateCard();

                if (card.LifeTime <= 0)
                {
                    sequence.Append(GameManager.Instance.GameBoard.RemoveFromPlayerHandTween(card));
                }
            }

            sequence.OnComplete(() =>
            {
                GameManager.Instance.Player.TakeDamage(1);
                
                GameManager.Instance.Player.CurrentEnergy = GameManager.Instance.GameBoard.GameRulesData.PlayerMaxMana;
                
                GameManager.Instance.UIManager.UpdateEnergy(GameManager.Instance.Player.CurrentEnergy);
                GameManager.Instance.GameFlowManager.SetState(new DrawState(gameManager));
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