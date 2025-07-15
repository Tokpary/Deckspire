using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects.EnemyActions
{
    [CreateAssetMenu(fileName = "DiscardRandomCard", menuName = "ScriptableObjects/Entity/EnemyAction/DiscardRandomCard", order = 1)]
    public class DiscardRandomCard : EnemyActionSO
    {
        public override void ExecuteAction(Enemy enemy)
        {
            int index = Random.Range(0, GameManager.Instance.GameBoard.PlayerHand.Count);
            // Remove the card from the player's hand
            GameManager.Instance.GameBoard.PlayerHand.RemoveAt(index);
        }
    }
}