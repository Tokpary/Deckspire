using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "OverchargeAbility", menuName = "ScriptableObjects/Card/Ability/OverchargeAbility", order = 1)]
    public class OverchargeAbility : CardAbilitySo
    {
        public int Increment;
        
        public override void Activate(ACard card)
        {
            GameManager.Instance.Player.CurrentEnergy += Increment;
        }
        
        public override void Deactivate(ACard card)
        {
        }
    }
}