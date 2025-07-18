using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects.EnemyPassives
{
    [CreateAssetMenu(fileName = "DrawOnEmptyHand", menuName = "ScriptableObjects/Entity/EnemyPassive/DrawOnEmptyHand", order = 1)]

    public class DrawOnEmptyHand : EnemyPassiveSO
    {

        public override void ActivatePassive(Enemy enemy)
        {
            GameManager.Instance.GameBoard.GameRulesData.DrawOnEmptyHandOnly = true;
        }

        public override void DeactivatePassive(Enemy enemy)
        {
            GameManager.Instance.GameBoard.GameRulesData.DrawOnEmptyHandOnly = false;
        }
    }
}