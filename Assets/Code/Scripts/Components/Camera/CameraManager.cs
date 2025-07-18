using System;
using Code.Scripts.Components.GameManagment;
using Code.Scripts.Components.GameManagment.GameStates;
using Code.Scripts.Components.Handdeck;
using Code.Scripts.DesignPatterns;
using DG.Tweening;
using UnityEngine;

namespace Code.Scripts.Components.Camera
{
    public class CameraManager : Singleton<CameraManager>
    {
        [SerializeField] private Transform topView;
        [SerializeField] private Transform tableView;
        [SerializeField] private Transform pivotPoint;
        
        [SerializeField] private float angleDegrees = 90f;
        [SerializeField] private float duration = 1f;
        
        private bool _isTopView = false;
        private bool _isAnimating = false;
        
        public void SubscribeToCardSelect()
        {
            GameManager.Instance.Player.HandDeck.OnCardSelected += MoveCameraToTopView;
            GameManager.Instance.Player.HandDeck.OnCardDeselected += HandleCardDeselected;
        }
        public void UnsubscribeFromCardSelect()
        {
            GameManager.Instance.Player.HandDeck.OnCardSelected -= MoveCameraToTopView;
            GameManager.Instance.Player.HandDeck.OnCardDeselected -= HandleCardDeselected;
        }

        private void Update()
        {
            if (GameManager.Instance.GameFlowManager.GetCurrentState() is not DeployCardState) return;
            if (_isAnimating) return;
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
            if (GameManager.Instance.GameFlowManager.GetCurrentState() is not DeployCardState) return;
            if (_isTopView) return;
            _isTopView = true;
            _isAnimating = true;
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
                                    obj.transform.DOLocalRotate(new Vector3(90, 0, 0), 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                                    {
                                        obj.CardStatus = CardStatus.Selected;
                                        _isAnimating = false;
                                    });
                                });
                        });
        }
        
        private void MoveCameraToTopView()
        {
            if (GameManager.Instance.GameFlowManager.GetCurrentState() is not DeployCardState) return;
            if (_isTopView) return;
            _isTopView = true;
            _isAnimating = true;
            
            float elapsed = 0f;

            DOTween.To(() => 0f, x =>
            {
                float deltaAngle = x - elapsed;
                transform.RotateAround(pivotPoint.position, Vector3.right, deltaAngle);
                elapsed = x;
            }, angleDegrees, duration).SetEase(Ease.InOutSine).OnComplete(() => {
                _isAnimating = false;
            });;
        }
        
        private void HandleCardDeselected(ACard obj)
        {
            if (GameManager.Instance.GameFlowManager.GetCurrentState() is not DeployCardState) return;
            if (!_isTopView) return;
            _isTopView = false;
            _isAnimating = true;
            float elapsed = 0f;
            
            DOTween.To(() => 0f, x => {
                float deltaAngle = x - elapsed;
                transform.RotateAround(pivotPoint.position, Vector3.right, -deltaAngle);
                elapsed = x;
            }, angleDegrees, duration).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                foreach (var card in GameManager.Instance.GameBoard.PlayerHand)
                {
                    card.CardStatus = CardStatus.InHand;
                }
                GameManager.Instance.Player.HandDeck.DeployCardsInHand();
                _isAnimating = false;
            });
            
                
        }

        public void ReturnToTableView()
        {
            if (!_isTopView) return;
            _isTopView = false;
            _isAnimating = true;
            float elapsed = 0f;
            DOTween.To(() => 0f, x => {
                float deltaAngle = x - elapsed;
                transform.RotateAround(pivotPoint.position, Vector3.right, -deltaAngle);
                elapsed = x;
            }, angleDegrees, duration).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                GameManager.Instance.Player.HandDeck.DeployCardsInHand();
                _isAnimating = false;
            });
        }
    }
}