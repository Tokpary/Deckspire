using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "PlanificationAbility", menuName = "ScriptableObjects/Card/Ability/PlanificationAbility", order = 1)]
    public class PlanificationAbility : CardAbilitySo
    {
        public int Increment;
        
        public override void Activate(ACard card, ACard actionCard = null)
        {
            foreach (ACard c in GameManager.Instance.GameBoard.PlayerHand)
            {
                c.LifeTime += Increment;
                c.UpdateCard();
            }
        }

        
        public override void Deactivate(ACard card)
        {
        }
    }
}