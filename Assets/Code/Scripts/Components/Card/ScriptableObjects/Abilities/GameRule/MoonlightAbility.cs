using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "MoonlightAbility", menuName = "ScriptableObjects/Card/Ability/MoonlightAbility", order = 1)]
    public class MoonlightAbility : CardAbilitySo
    {

        public override void Activate(ACard card, ACard actionCard = null)
        {
            GameManager.Instance.GameBoard.GameRulesData.IsMoonLightApplied = true;
        }
        
        public override void Deactivate(ACard card)
        {
            GameManager.Instance.GameBoard.GameRulesData.IsMoonLightApplied = false;
        }
    }
}