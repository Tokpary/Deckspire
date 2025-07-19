using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "TransfusionAbility", menuName = "ScriptableObjects/Card/Ability/TransfusionAbility", order = 1)]
    public class TransfusionAbility : CardAbilitySo
    {
        public override void Activate(ACard targetCard, ACard actionCard = null)
        {
            if (actionCard == null)
                return;
            
            targetCard.LifeTime += actionCard.LifeTime;
            targetCard.UpdateCard();
        }

        public override void Deactivate(ACard card)
        {
        }
    }
}