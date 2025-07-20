using System;
using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects.EnemyDeathEvents
{
    [CreateAssetMenu(fileName = "FreezeOnHit", menuName = "ScriptableObjects/Entity/DeathEvents/FreezeOnHit", order = 1)]
    public class FreezeOnHit : DieEventSO
    {
        public override void OnDieEvent(Action onComplete = null)
        {
            if (GameManager.Instance == null || GameManager.Instance.GameBoard == null)
            {
                onComplete?.Invoke();
                return;
            }

            GameManager.Instance.GameBoard.GameRulesData.NextRoundIsFreeze = true;
            onComplete?.Invoke();
        }
    }
}