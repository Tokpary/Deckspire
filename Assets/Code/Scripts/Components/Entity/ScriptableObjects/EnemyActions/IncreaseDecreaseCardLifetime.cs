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
            foreach (ACard card in GameManager.Instance.GameBoard.PlayerHand)
            {
                card.LifeTime += Random.Range(-1, 1);
            }
        }
    }
}