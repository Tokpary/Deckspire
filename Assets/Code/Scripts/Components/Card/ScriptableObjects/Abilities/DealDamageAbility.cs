
using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "DealDamageAbility", menuName = "ScriptableObjects/Card/Ability/DealDamageAbility", order = 1)]
    public class DealDamageAbility : CardAbilitySo
    {
        public int damage;

        public override void Activate(ACard targetCard, ACard actionCard = null)
        {
            if (GameManager.Instance.GameBoard.GameRulesData.IsFriendlyFireApplied)
            {
                targetCard.LifeTime -= damage;
                targetCard.UpdateCard();
            }
            else
            {
                GameManager.Instance.Enemy.TakeDamage(damage);
            }
        }
        
        public override void Deactivate(ACard card)
        {
        }
    }
}