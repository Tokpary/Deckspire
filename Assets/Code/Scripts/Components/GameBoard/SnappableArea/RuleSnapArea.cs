using System;
using Code.Scripts.Components.Interfaces;
using UnityEngine;

namespace Code.Scripts.Components.GameBoard.SnappableArea
{
    public class RuleSnapArea : MonoBehaviour, ISnapZone
    {
        private Transform _snapPoint;

        private void Awake()
        {
            _snapPoint = transform;
        }

        public bool CanAcceptCard(ACard card)
        {
            Debug.Log($"Checking if card can be accepted: {card.GetDataCard().cardType}");
            if (card.GetDataCard().cardType == 2)
            {
                return true;
            }

            return false;
        }

        public void SnapCard(ACard card)
        {
            if (card == null) return;

            if (CanAcceptCard(card))
            {
                card.transform.position = _snapPoint.position;
                card.PlayCard();
            }
        }
    }
}