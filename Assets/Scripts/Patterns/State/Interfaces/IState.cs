using Code.Scripts.Components.GameManagment;

namespace Patterns.State.Interfaces
{
    public interface IState
    {
        public void Enter(IGameState gameManager);
        public void Exit(IGameState gameManager);
        public void Update();
    }
}