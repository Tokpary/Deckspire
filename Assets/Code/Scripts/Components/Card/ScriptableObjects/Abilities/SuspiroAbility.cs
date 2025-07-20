using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
        [CreateAssetMenu(fileName = "SuspiroAbility", menuName = "ScriptableObjects/Card/Ability/SuspiroAbility", order = 1)]
        public class SuspiroAbility : CardAbilitySo
        {
            public int amount;

            public override void Activate(ACard card, ACard actionCard = null)
            {
                GameManager.Instance.Player.TakeDamage(-amount);
            }
        
            public override void Deactivate(ACard card)
            {
            }
        }
    
}