using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects.EnemyActions
{
    
    [CreateAssetMenu(fileName = "NewAddGarbageAction", menuName = "ScriptableObjects/Entity/EnemyAction/AddGarbageCards", order = 1)]
    public class AddGarbageCards : EnemyActionSO
    {
        public override void ExecuteAction(Enemy enemy)
        {
           // gameContext.currentPlayer.HandDeck
        }
    }
}