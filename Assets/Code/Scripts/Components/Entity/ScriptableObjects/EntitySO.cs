
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects
{
    
    [CreateAssetMenu(fileName = "NewEntity", menuName = "ScriptableObjects/Entity/Entity", order = 1)]
    public class EntitySO : ScriptableObject
    {
        public string Name;
        public int MaxHealth;
        public List<EnemyActionSO> Actions;
        public List<EnemyPassivesSO>  Passives;
    }
}