using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects
{
    
    public abstract class EnemyActionSO : ScriptableObject
    {
        public abstract void ExecuteAction(Enemy enemy);
    }
}