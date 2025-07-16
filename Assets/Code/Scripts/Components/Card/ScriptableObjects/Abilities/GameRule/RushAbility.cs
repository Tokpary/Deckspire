using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "RushAbility", menuName = "ScriptableObjects/Card/Ability/RushAbility", order = 1)]
    public class RushAbility : CardAbilitySo
    {
        public int energyIncrement;

        public override void Activate(ACard card)
        {
            GameManager.Instance.GameBoard.GameRulesData.MaxEnergy += energyIncrement;
        }
        
        public override void Deactivate(ACard card)
        {
            GameManager.Instance.GameBoard.GameRulesData.MaxEnergy -= energyIncrement;
        }
    }
}