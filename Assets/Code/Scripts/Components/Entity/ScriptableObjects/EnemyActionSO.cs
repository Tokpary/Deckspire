using System;
using Code.Scripts.Components.GameManagment;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects
{
    
    public abstract class EnemyActionSO : ScriptableObject
    {
        public abstract void ExecuteAction(Action onComplete = null);
    }
}