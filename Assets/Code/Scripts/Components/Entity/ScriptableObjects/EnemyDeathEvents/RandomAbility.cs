using System;
using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects.EnemyDeathEvents
{
    [CreateAssetMenu(fileName = "RandomAbility", menuName = "ScriptableObjects/Entity/DeathEvents/RandomAbility", order = 1)]
    public class RandomAbility : DieEventSO
    {
        public override void OnDieEvent(Action onComplete = null)
        {
            if (GameManager.Instance == null || GameManager.Instance.GameBoard == null)
            {
                onComplete?.Invoke();
                return;
            }

            GameManager.Instance.Enemy.PlayTurn();
            GameManager.Instance.Enemy.RestartLife();
        }
    }
}