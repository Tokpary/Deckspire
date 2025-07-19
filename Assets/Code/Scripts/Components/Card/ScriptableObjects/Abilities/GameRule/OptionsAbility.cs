using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "OptionsAbility", menuName = "ScriptableObjects/Card/Ability/OptionsAbility", order = 1)]
    public class OptionsAbility : CardAbilitySo
    {
        public int handSizeIncrement;

        public override void Activate(ACard card, ACard actionCard = null)
        {
            GameManager.Instance.GameBoard.GameRulesData.MaxHandSize += handSizeIncrement;
        }
        
        public override void Deactivate(ACard card)
        {
            GameManager.Instance.GameBoard.GameRulesData.MaxHandSize -= handSizeIncrement;
        }
    }
}