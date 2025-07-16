using Code.Scripts.Components.GameManagment;
using Code.Scripts.Components.GameManagment.GameStates;
using Code.Scripts.Components.Interfaces;
using UnityEngine;

namespace Code.Scripts.Components.Interactables
{
    public class InteractableTurnPass : AInteractable
    {
        public override void Activate()
        {
         if(GameManager.Instance.GameFlowManager.GetCurrentState() is DeployCardState);
            GameManager.Instance.GameFlowManager.SetState(new EnemyActionState(GameManager.Instance.GameFlowManager));
        }

        public override void Highlight()
        {
        
        }
    }
}