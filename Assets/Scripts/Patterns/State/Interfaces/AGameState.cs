using Code.Scripts.Components.GameManagment;

namespace Patterns.State.Interfaces
{
    public abstract class AGameState : IState
    {
        protected IGameState _gameState;
        
        public AGameState(IGameState gameState)
        {
            this._gameState = gameState;
        }

        public abstract void Enter(IGameState gameManager);

        public abstract void Exit(IGameState gameManager);
        
        public abstract void Update();

    }
}