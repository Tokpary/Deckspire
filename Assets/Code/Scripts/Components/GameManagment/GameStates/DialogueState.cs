using Patterns.State.Interfaces;

namespace Code.Scripts.Components.GameManagment.GameStates
{
    public class DialogueState : AGameState
    {
        private readonly string message;
        public DialogueState(IGameState gameState, string message) : base(gameState)
        {
            this.message = message;
        }

        public override void Enter(IGameState gameManager)
        {
            Fungus.Flowchart.BroadcastFungusMessage(message);
        }

        public override void Exit(IGameState gameManager)
        {
            //
        }

        public override void Update()
        {
            //
        }
    }
}