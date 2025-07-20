using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects.EnemyActions
{
    [CreateAssetMenu(fileName = "ReduceEnergy", menuName = "ScriptableObjects/Entity/EnemyAction/ReduceEnergy", order = 1)]
    public class ReduceEnergy : EnemyActionSO
    {
        public override void ExecuteAction(System.Action onComplete = null)
        {
            base.ExecuteAction();
            GameManager.Instance.GameBoard.GameRulesData.NextRoundIsEnergyLoss = true;
            onComplete?.Invoke();
        }
    }
}