using Patterns.State.Interfaces;

namespace Code.Scripts.Components.GameManagment.GameStates
{
    public class EnemyActionState : AGameState
    {
        public EnemyActionState(GameManager gameState) : base(gameState)
        {
        }

        public override void Enter(GameManager gameManager)
        {
            gameManager.Enemy.PlayTurn(gameManager);
        }

        public override void Exit(GameManager gameManager)
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}