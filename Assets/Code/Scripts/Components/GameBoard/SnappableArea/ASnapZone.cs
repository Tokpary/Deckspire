using Code.Scripts.Components.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace Code.Scripts.Components.GameBoard.SnappableArea
{
    public abstract class ASnapZone : MonoBehaviour, ISnapZone
    {
        protected ACard _currentCardOnSlot;
        [SerializeField] private Transform _discardStack;
        public abstract bool CanAcceptCard(ACard card);

        public abstract void SnapCard(ACard card);

        public virtual void RemoveCardFromSlot()
        {
            _currentCardOnSlot.transform.DOMove(_discardStack.position, 0.5f)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    _currentCardOnSlot.transform.rotation = Quaternion.identity;
                    _currentCardOnSlot = null;
                });
        }
    }
}