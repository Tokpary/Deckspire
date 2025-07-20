using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "NecromancyAbility", menuName = "ScriptableObjects/Card/Ability/NecromancyAbility", order = 1)]
    public class NecromancyAbility : CardAbilitySo
    {
        public override void Activate(ACard card, ACard actionCard = null)
        {
            GameManager.Instance.GameBoard.GameRulesData.IsNecromancyApplied = true;
        }

        public override void Deactivate(ACard card)
        {
            GameManager.Instance.GameBoard.GameRulesData.IsNecromancyApplied = false;
        }
    }
}