using Patterns.State.Interfaces;

namespace Code.Scripts.Components.GameManagment.GameStates
{
    public class DialogueState : AGameState
    {
        public DialogueState(IGameState gameState) : base(gameState)
        {
        }

        public override void Enter(IGameState gameManager)
        {
            throw new System.NotImplementedException();
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