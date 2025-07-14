using Code.Scripts.Components.GameManagment;

namespace Patterns.State.Interfaces
{
    public interface IState
    {
        public void Enter(GameManager gameManager);
        public void Exit(GameManager gameManager);
        public void Update();
    }
}