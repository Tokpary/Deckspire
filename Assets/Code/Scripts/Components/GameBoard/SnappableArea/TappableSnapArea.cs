using Code.Scripts.Components.GameManagment;
using Code.Scripts.Components.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Code.Scripts.Components.GameBoard.SnappableArea
{
    public class TappableSnapArea : ASnapZone, ISnapZone
    {
        private Transform _snapPoint;

        private void Awake()
        {
            _snapPoint = transform;
        }

        public override bool CanAcceptCard(ACard card)
        {
            if (card.GetDataCard().cardType == 1 && card.EnergyCost <= GameManager.Instance.Player.CurrentEnergy)
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
                card.transform.DORotate(new Vector3(90,90, 0), 0.5f).SetEase(Ease.OutBack);
                _currentCardOnSlot = card;
                GameManager.Instance.Player.CurrentEnergy -= card.EnergyCost;
                GameManager.Instance.GameBoard.DisplayCardInTable(card);
            }
        }
    }
}