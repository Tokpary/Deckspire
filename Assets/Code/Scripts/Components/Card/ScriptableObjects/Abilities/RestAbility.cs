using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "RestAbility", menuName = "ScriptableObjects/Card/Ability/RestAbility", order = 1)]
    public class RestAbility : CardAbilitySo
    {
        [SerializeField] private int increment = 1;
        public override void Activate(ACard card, ACard actionCard = null) // targetCard
        {
            card.LifeTime += increment;
            card.UpdateCard();
        }

        public override void Deactivate(ACard card)
        {
        }
    }
}