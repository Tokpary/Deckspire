using Patterns.State.Interfaces;
using UnityEngine;

namespace Code.Scripts.Components.GameManagment.GameStates
{
    public class DrawState : AGameState
    {
        public DrawState(IGameState gameState) : base(gameState)
        {
            
        }

        public override void Enter(IGameState gameManager)
        {
            
            var gm = GameManager.Instance;
            var f = gameManager;
            gm.GameBoard.RefillPlayerHand(() =>
            {
                f.SetState(new DeployCardState(f));  
            });
        }

        public override void Exit(IGameState gameManager)
        {
        }

        public override void Update()
        {
        }
    }
}