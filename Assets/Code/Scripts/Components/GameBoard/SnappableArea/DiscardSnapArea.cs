using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.GameBoard.SnappableArea
{
    public class DiscardSnapArea : ASnapZone
    {
        Transform _snapPoint;

        private void Awake()
        {
            _snapPoint = transform;
        }
        public override bool CanAcceptCard(ACard card)
        {
            if (card.GetDataCard().cardType == 0 && card.EnergyCost <= GameManager.Instance.Player.CurrentEnergy)
            {
                return true;
            }

            return false;
        }

        public override void SnapCard(ACard card)
        {
            if (card == null) return;

            if (CanAcceptCard(card))
            {
                card.transform.position = _snapPoint.position;
                if (_currentCardOnSlot != null)
                {
                    _currentCardOnSlot.transform.position = new Vector3(0.829f, 1.81f, -2.274f);
                    _currentCardOnSlot.transform.rotation = Quaternion.Euler(90, 0, 180);
                }
                _currentCardOnSlot = card;
                GameManager.Instance.Player.CurrentEnergy -= card.EnergyCost;
                GameManager.Instance.GameBoard.DisplayCardInTable(card);
                card.PlayCard();
            }
        }
    }
}