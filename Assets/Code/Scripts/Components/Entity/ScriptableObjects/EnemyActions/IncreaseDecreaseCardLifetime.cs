using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects.EnemyActions
{
    [CreateAssetMenu(fileName = "IncreaseDecreaseCardLifetime", menuName = "ScriptableObjects/Entity/EnemyAction/IncreaseDecreaseCardLifetime", order = 1)]
    public class IncreaseDecreaseCardLifetime : EnemyActionSO
    {

        public override void ExecuteAction(Enemy enemy)
        {
            foreach (ACard card in GameManager.Instance.GameBoard.PlayerHand)
            {
                card.LifeTime += Random.Range(-1, 1);
            }
        }
    }
}