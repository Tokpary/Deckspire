using System;
using Code.Scripts.Components.GameManagment;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.Components.Entity.ScriptableObjects.EnemyActions
{
    [CreateAssetMenu(fileName = "IncrementRandomCardLifetime", menuName = "ScriptableObjects/Entity/EnemyAction/IncrementRandomCardLifetime", order = 1)]
    public class IncrementRandomCardLifetime : EnemyActionSO
    {
        public int IncrementAmount = 2;
        public override void ExecuteAction(Action onComplete = null)
        {
            if (GameManager.Instance.GameBoard.PlayerHand.Count == 0)
            {
                onComplete?.Invoke();
                return;
            }

            int index = Random.Range(0, GameManager.Instance.GameBoard.PlayerHand.Count);
            ACard card = GameManager.Instance.GameBoard.PlayerHand[index];
            
            card.LifeTime += IncrementAmount;
            card.UpdateCard();
            onComplete?.Invoke();
        }
    }
}