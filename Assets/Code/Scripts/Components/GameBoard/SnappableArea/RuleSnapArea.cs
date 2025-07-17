using System;
using Code.Scripts.Components.GameManagment;
using Code.Scripts.Components.Interfaces;
using UnityEngine;

namespace Code.Scripts.Components.GameBoard.SnappableArea
{
    public class RuleSnapArea : ASnapZone
    {
        Transform _snapPoint;

        private void Awake()
        {
            _snapPoint = transform;
        }

        public override bool CanAcceptCard(ACard card)
        {
            if (card.GetDataCard().cardType == 2 && card.EnergyCost <= GameManager.Instance.Player.CurrentEnergy)
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
                _currentCardOnSlot = card;
                GameManager.Instance.Player.CurrentEnergy -= card.EnergyCost;
                GameManager.Instance.GameBoard.DisplayCardInTable(card);
                card.PlayCard();
            }
        }
        
        
    }
}