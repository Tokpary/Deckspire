using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "ProlongationAbility", menuName = "ScriptableObjects/Card/Ability/ProlongationAbility", order = 1)]
    public class ProlongationAbility : CardAbilitySo
    {
        public int Increment;
        public override void Activate(ACard card, ACard actionCard = null)
        {
            card.LifeTime += Increment;
            card.UpdateCard();
        }

        public override void Deactivate(ACard card)
        {
        }
    }
}