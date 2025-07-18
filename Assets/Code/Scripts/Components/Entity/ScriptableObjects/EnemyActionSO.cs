using System;
using Code.Scripts.Components.GameManagment;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects
{
    
    public abstract class EnemyActionSO : ScriptableObject
    {
        [SerializeField] private string actionDialogue = "Adding garbage card to your deck!";

        public virtual void ExecuteAction(Action onComplete = null)
        {
            SayDialogue();
        }
        
        public virtual void SayDialogue(Action onComplete = null)
        {
            GameManager.Instance.DialogueManager.DisplayDialogue(actionDialogue, () =>
            {
                onComplete?.Invoke();
            });
        }
    }
}