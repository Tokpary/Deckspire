using Code.Scripts.Components.GameManagment;

namespace Patterns.State.Interfaces
{
    public abstract class AGameState : IState
    {
        protected IGameState _gameState;
        
        public AGameState(GameManager gameState)
        {
            this._gameState = gameState;
        }

        public abstract void Enter(GameManager gameManager);

        public abstract void Exit(GameManager gameManager);
        
        public abstract void Update();

    }
}