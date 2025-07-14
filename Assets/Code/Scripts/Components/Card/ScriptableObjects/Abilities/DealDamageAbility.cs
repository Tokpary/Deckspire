
using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "DealDamageAbility", menuName = "ScriptableObjects/Card/Ability/DealDamageAbility", order = 1)]
    public class DealDamageAbility : CardAbilitySo
    {
        public int damage;

        public override void Activate(ACard card, GameContext gameContext)
        {
            gameContext.targetEnemy.TakeDamage(damage);
        }
    }
}