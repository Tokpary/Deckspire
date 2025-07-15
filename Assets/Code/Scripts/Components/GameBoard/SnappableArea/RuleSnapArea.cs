using Code.Scripts.Components.Interfaces;
using UnityEngine;

namespace Code.Scripts.Components.GameBoard.SnappableArea
{
    public class RuleSnapArea : MonoBehaviour, ISnapZone
    {
        public bool CanAcceptCard(ACard card)
        {
            if (card.GetDataCard())
            {
                
            }

            return true;
        }

        public void SnapCard(ACard card)
        {
            throw new System.NotImplementedException();
        }
    }
}