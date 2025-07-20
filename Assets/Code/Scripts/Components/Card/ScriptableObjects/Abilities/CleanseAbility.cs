using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "CleanseAbility", menuName = "ScriptableObjects/Card/Ability/CleanseAbility", order = 1)]
    public class CleanseAbility : CardAbilitySo
    {
        public override void Activate(ACard card, ACard actionCard = null)
        {
            GameManager.Instance.GameBoard.GameRulesData.IsCleanseApplied = true;
        }

        public override void Deactivate(ACard card)
        {
            throw new System.NotImplementedException();
        }
    }
}