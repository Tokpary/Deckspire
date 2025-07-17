using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "ReinforcmentAbility", menuName = "ScriptableObjects/Card/Ability/ReinforcmentAbility", order = 1)]
    public class ReinforcmentAbility : CardAbilitySo
    {
        public int Multiplier;

        public override void Activate(ACard card)
        {
            GameManager.Instance.Enemy.DamageMultiplier = Multiplier;
        }

        public override void Deactivate(ACard card)
        {
            
        }
    }
}