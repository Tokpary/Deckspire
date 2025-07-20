using System;
using Code.Scripts.Components.GameManagment;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.Components.Entity.ScriptableObjects.EnemyActions
{
    [CreateAssetMenu(fileName = "DiscardRandomCard", menuName = "ScriptableObjects/Entity/EnemyAction/DiscardRandomCard", order = 1)]
    public class DiscardRandomCard : EnemyActionSO
    {
        public override void ExecuteAction(Action onComplete = null)
        {
            
            if (GameManager.Instance.GameBoard.PlayerHand.Count == 0)
            {
                onComplete?.Invoke();
                return;
            }

            base.ExecuteAction();
            int index = Random.Range(0, GameManager.Instance.GameBoard.PlayerHand.Count);
            ACard card = GameManager.Instance.GameBoard.PlayerHand[index];

            Sequence s = GameManager.Instance.GameBoard.RemoveFromPlayerHandTween(card);
            s.OnComplete(() =>
            {
                onComplete?.Invoke();
            });
            s.Play();
        }
    }
}