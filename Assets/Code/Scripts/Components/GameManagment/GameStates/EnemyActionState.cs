using Patterns.State.Interfaces;
using UnityEngine;

namespace Code.Scripts.Components.GameManagment.GameStates
{
    public class EnemyActionState : AGameState
    {
        public EnemyActionState(IGameState gameState) : base(gameState)
        {
        }

        public override void Enter(IGameState gameState)
        {
            if (GameManager.Instance.GameBoard.GameRulesData.IsSkippingNextEnemy)
            {
                GameManager.Instance.GameBoard.GameRulesData.IsSkippingNextEnemy = false;
                GameManager.Instance.GameFlowManager.SetState(new AfterEnemyState(GameManager.Instance.GameFlowManager));
                return;
            }
            else
            {
                GameManager.Instance.TurnManager.EnemyTurn();
            }
        }

        public override void Exit(IGameState gameManager)
        {
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}