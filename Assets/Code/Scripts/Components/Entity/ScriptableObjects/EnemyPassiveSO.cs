using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects
{
    
    public abstract class EnemyPassiveSO : ScriptableObject
    {
        public abstract void ActivatePassive(Enemy enemy);
        public abstract void DeactivatePassive(Enemy enemy);
    }
}