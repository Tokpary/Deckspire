using System;
using Code.Scripts.Components.Handdeck;
using DG.Tweening;
using UnityEngine;

namespace Code.Scripts.Components.Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Transform topView;
        [SerializeField] private Transform tableView;
        private void OnEnable()
        {
            HandDeckManager.Instance.OnCardSelected += MoveCameraToTopView;
            HandDeckManager.Instance.OnCardDeselected += HandleCardDeselected;
        }

        private void MoveCameraToTopView(ACard obj)
        {
            transform.DOMove(topView.position, 0.5f)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    transform.DORotate(topView.rotation.eulerAngles, 0.5f)
                        .SetEase(Ease.OutBack).OnComplete(() =>
                        {
                            obj.transform.DOMove(transform.position + transform.forward * 0.5f - transform.up * 0.1f, 0.5f)
                                .SetEase(Ease.OutBack).OnComplete(() =>
                                {
                                    obj.transform.DOLocalRotate(new Vector3(90, 0, 0), 0.5f).SetEase(Ease.OutBack);
                                });
                        });
                });
        }
        
        private void HandleCardDeselected(ACard obj)
        {
            transform.DOMove(tableView.position, 0.5f)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    transform.DORotate(tableView.rotation.eulerAngles, 0.5f)
                        .SetEase(Ease.OutBack).OnComplete(HandDeckManager.Instance.DeployCardsInHand);
                });
        }

        private void OnDisable()
        {
            HandDeckManager.Instance.OnCardSelected -= MoveCameraToTopView;
            HandDeckManager.Instance.OnCardDeselected -= HandleCardDeselected;
        }
    }
}