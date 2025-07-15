using Code.Scripts.Components.Camera;
using Code.Scripts.Components.Card.ScriptableObjects;
using Patterns.State.Interfaces;
using UnityEngine;

namespace Code.Scripts.Components.GameManagment.GameStates
{
    public class DeployCardState : AGameState
    {
        public DeployCardState(IGameState gameState) : base(gameState)
        {
        }

        public override void Enter(IGameState gameManager)
        {
            CameraManager.Instance.SubscribeToCardSelect();
            Debug.Log("DeployCardState: Entering Deploy Card State. Player can now deploy cards.");
        }

        public override void Exit(IGameState gameManager)
        {
            CameraManager.Instance.UnsubscribeFromCardSelect();
            // Logic for exiting the deploy card state
        }

        public override void Update()
        {
            // Logic for updating the deploy card state
        }
    }
}