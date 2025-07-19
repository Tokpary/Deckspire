using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "FriendlyFireAbility", menuName = "ScriptableObjects/Card/Ability/FriendlyFireAbility", order = 1)]
    public class FriendlyFireAbility : CardAbilitySo
    {

        public override void Activate(ACard card, ACard actionCard = null)
        {
            GameManager.Instance.GameBoard.GameRulesData.IsFriendlyFireApplied = true;
        }
        
        public override void Deactivate(ACard card)
        {
            GameManager.Instance.GameBoard.GameRulesData.IsFriendlyFireApplied = false;
        }
    }
}