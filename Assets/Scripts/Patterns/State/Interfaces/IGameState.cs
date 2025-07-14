using System.Collections.Generic;
using UnityEngine;

namespace Patterns.State.Interfaces
{
    public interface IGameState
    {
        
        public void StartGame();
        public void EndGame();
        public IState GetCurrentState();
        public void SetState(IState state);
    }
}