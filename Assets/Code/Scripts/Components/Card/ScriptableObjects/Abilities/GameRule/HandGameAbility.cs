using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects.Abilities
{
    [CreateAssetMenu(fileName = "HandGameAbility", menuName = "ScriptableObjects/Card/Ability/HandGameAbility", order = 1)]
    public class HandGameAbility : CardAbilitySo
    {

        public override void Activate(ACard card, ACard actionCard = null)
        {
            GameManager.Instance.GameBoard.ExtraSlot.gameObject.SetActive(true);
        }
        
        public override void Deactivate(ACard card)
        {
            if(GameManager.Instance.GameBoard.ExtraSlot.CurrentCardOnSlot != null)
            {
                ACard actionCard = GameManager.Instance.GameBoard.ExtraSlot.CurrentCardOnSlot;
                GameManager.Instance.GameBoard.MoveCardToDiscard(actionCard);
            }
            GameManager.Instance.GameBoard.ExtraSlot.gameObject.SetActive(false);
        }
    }
}