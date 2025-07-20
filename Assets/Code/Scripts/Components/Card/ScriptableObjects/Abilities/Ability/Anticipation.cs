using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "AnticipationAbility", menuName = "ScriptableObjects/Card/Ability/AnticipationAbility", order = 1)]
    public class AnticipationAbility : CardAbilitySo
    {
        public override void Activate(ACard card, ACard actionCard = null)
        {
            GameManager.Instance.GameBoard.GameRulesData.IsSkippingNextEnemy = true;
        }
        
        public override void Deactivate(ACard card)
        {
        }
    }
}