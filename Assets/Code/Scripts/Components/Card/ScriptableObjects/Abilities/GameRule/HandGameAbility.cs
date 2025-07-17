using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "HandGameAbility", menuName = "ScriptableObjects/Card/Ability/HandGameAbility", order = 1)]
    public class HandGameAbility : CardAbilitySo
    {

        public override void Activate(ACard card)
        {
            GameManager.Instance.GameBoard.GameRulesData.IsHandGameApplied = true;
        }
        
        public override void Deactivate(ACard card)
        {
            GameManager.Instance.GameBoard.GameRulesData.IsHandGameApplied = false;
        }
    }
}