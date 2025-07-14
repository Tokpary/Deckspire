using Patterns.State.Interfaces;

namespace Code.Scripts.Components.GameManagment.GameStates
{
    public class MenuState : AGameState
    {
        public MenuState(GameManager gameState) : base(gameState)
        {
        }

        public override void Enter(GameManager gameManager)
        {
            // Logic for entering the menu state
            // This could include showing the main menu UI, pausing the game, etc.
            gameManager.ShowMainMenu();
        }

        public override void Exit(GameManager gameManager)
        {
            // Logic for exiting the menu state
            // This could include hiding the main menu UI, resuming the game, etc.
            gameManager.HideMainMenu();
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}