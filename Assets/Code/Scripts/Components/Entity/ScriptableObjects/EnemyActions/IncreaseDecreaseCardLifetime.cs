using System;
using Code.Scripts.Components.GameManagment;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.Components.Entity.ScriptableObjects.EnemyActions
{
    [CreateAssetMenu(fileName = "IncreaseDecreaseCardLifetime", menuName = "ScriptableObjects/Entity/EnemyAction/IncreaseDecreaseCardLifetime", order = 1)]
    public class IncreaseDecreaseCardLifetime : EnemyActionSO
    {

        public override void ExecuteAction(Action onComplete = null)
        {
            base.ExecuteAction();
            foreach (ACard card in GameManager.Instance.GameBoard.PlayerHand)
            {
                int randomValue = Random.Range(-1, 4); 
                card.LifeTime += randomValue;
                Debug.Log($"Card: {card.LifeTime}");
                card.UpdateCard();
            }
            onComplete?.Invoke();
        }
    }
}