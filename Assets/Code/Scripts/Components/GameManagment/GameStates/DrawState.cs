using Patterns.State.Interfaces;

namespace Code.Scripts.Components.GameManagment.GameStates
{
    public class DrawState : AGameState
    {
        public DrawState(GameManager gameState) : base(gameState)
        {
            
        }

        public override void Enter(GameManager gameManager)
        {
            gameManager.gameBoard.RefillPlayerHand();
        }

        public override void Exit(GameManager gameManager)
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
        }
    }
}