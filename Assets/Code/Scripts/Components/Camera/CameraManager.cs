using System;
using Code.Scripts.Components.GameManagment;
using Code.Scripts.Components.Handdeck;
using DG.Tweening;
using UnityEngine;

namespace Code.Scripts.Components.Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Transform topView;
        [SerializeField] private Transform tableView;
        [SerializeField] private Transform pivotPoint;
        
        [SerializeField] private float angleDegrees = 90f;
        [SerializeField] private float duration = 1f;
        
        private bool _isTopView = false;
        
        private void OnEnable()
        {
            GameManager.Instance.Player.HandDeck.OnCardSelected += MoveCameraToTopView;
            GameManager.Instance.Player.HandDeck.OnCardDeselected += HandleCardDeselected;
        }

        private void Update()
        {
            if (GameManager.Instance.GetCurrentState() is not DeployCardState) return;
            if (Input.GetKeyDown(KeyCode.W))
            {
                MoveCameraToTopView();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                HandleCardDeselected(null);
            }
        }

        private void MoveCameraToTopView(ACard obj)
        {
            if (GameManager.Instance.GetCurrentState() is not DeployCardState) return;
            if (_isTopView) return;
            _isTopView = true;
            float elapsed = 0f;
            
            DOTween.To(() => 0f, x => {
                float deltaAngle = x - elapsed;
                transform.RotateAround(pivotPoint.position, Vector3.right, deltaAngle);
                elapsed = x;
            }, angleDegrees, duration).SetEase(Ease.InOutSine).OnComplete(() =>
                        {
                            obj.transform.DOMove(transform.position + transform.forward * 0.5f - transform.up * 0.1f, 0.5f)
                                .SetEase(Ease.OutBack).OnComplete(() =>
                                {
                                    obj.transform.DOLocalRotate(new Vector3(90, 0, 0), 0.5f).SetEase(Ease.OutBack);
                                });
                        });
        }
        
        private void MoveCameraToTopView()
        {
            float elapsed = 0f;

            DOTween.To(() => 0f, x =>
            {
                float deltaAngle = x - elapsed;
                transform.RotateAround(pivotPoint.position, Vector3.right, deltaAngle);
                elapsed = x;
            }, angleDegrees, duration).SetEase(Ease.InOutSine);
        }
        
        private void HandleCardDeselected(ACard obj)
        {
            
            if (GameManager.Instance.GetCurrentState() is not DeployCardState) return;
            if (!_isTopView) return;
            _isTopView = false;
            float elapsed = 0f;
            
            DOTween.To(() => 0f, x => {
                float deltaAngle = x - elapsed;
                transform.RotateAround(pivotPoint.position, Vector3.right, -deltaAngle);
                elapsed = x;
            }, angleDegrees, duration).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                transform.DORotate(tableView.rotation.eulerAngles, 0.5f)
                    .SetEase(Ease.OutBack).OnComplete(GameManager.Instance.Player.HandDeck.DeployCardsInHand);
            });
            
                
        }

        private void OnDisable()
        {
            GameManager.Instance.Player.HandDeck.OnCardSelected -= MoveCameraToTopView;
            GameManager.Instance.Player.HandDeck.OnCardDeselected -= HandleCardDeselected;
        }
    }
}