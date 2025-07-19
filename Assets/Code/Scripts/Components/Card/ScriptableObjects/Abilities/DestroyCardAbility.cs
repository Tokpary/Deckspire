using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "DestroyCardAbility", menuName = "ScriptableObjects/Card/Ability/DestroyCardAbility", order = 1)]
    public class DestroyCardAbility : CardAbilitySo
    {
        public override void Activate(ACard card, ACard actionCard = null)
        {
            if (actionCard == null)
                return;

            GameManager.Instance.GameBoard.DestroyCard(card);
        }

        public override void Deactivate(ACard card)
        {
        }
    }
}