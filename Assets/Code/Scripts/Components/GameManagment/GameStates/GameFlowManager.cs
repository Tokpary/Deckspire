
using Patterns.State.Interfaces;
using UnityEngine;

namespace Code.Scripts.Components.GameManagment.GameStates
{
    public class GameFlowManager : MonoBehaviour, IGameState
    {
        private IState _currentState;
        private IState _previousState;
        private GameManager _gameManager;
        
        private void Awake()
        {
            _gameManager = GetComponent<GameManager>();
        }
        
        public void StartGame()
        {
            throw new System.NotImplementedException();
        }

        public void EndGame()
        {
            throw new System.NotImplementedException();
        }

        public IState GetCurrentState()
        {
            return _currentState;
        }

        public void SetState(IState state)
        {
            if (_currentState != null)
            {
                _currentState.Exit(this);
            }

            _previousState = _currentState;
            _currentState = state;
            _currentState.Enter(this);
        }
    }
}