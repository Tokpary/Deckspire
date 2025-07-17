using Patterns.State.Interfaces;

namespace Code.Scripts.Components.GameManagment.GameStates
{
    public class MenuState : AGameState
    {
        public MenuState(IGameState gameState) : base(gameState)
        {
        }

        public override void Enter(IGameState gameManager)
        {
            // Logic for entering the menu state
            // This could include showing the main menu UI, pausing the game, etc.
            GameManager.Instance.ShowMainMenu();
        }

        public override void Exit(IGameState gameManager)
        {
            // Logic for exiting the menu state
            // This could include hiding the main menu UI, resuming the game, etc.
            GameManager.Instance.HideMainMenu();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}