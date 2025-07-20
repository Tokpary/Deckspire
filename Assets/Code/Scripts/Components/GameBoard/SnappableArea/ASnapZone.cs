using Code.Scripts.Components.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Code.Scripts.Components.GameBoard.SnappableArea
{
    public abstract class ASnapZone : MonoBehaviour, ISnapZone
    {
        protected ACard _currentCardOnSlot;
        public ACard CurrentCardOnSlot
        {
            get => _currentCardOnSlot;
            set => _currentCardOnSlot = value;
        }
        [SerializeField] private Transform _discardStack;
        public abstract bool CanAcceptCard(ACard card);

        public abstract void SnapCard(ACard card);

        public virtual void RemoveCardFromSlot()
        {
            _currentCardOnSlot = null;
        }
    }
}