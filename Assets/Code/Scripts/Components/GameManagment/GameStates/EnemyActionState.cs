using Patterns.State.Interfaces;

namespace Code.Scripts.Components.GameManagment.GameStates
{
    public class EnemyActionState : AGameState
    {
        public EnemyActionState(IGameState gameState) : base(gameState)
        {
        }

        public override void Enter(IGameState gameState)
        {
            var gm = GameManager.Instance;
        }

        public override void Exit(IGameState gameManager)
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}