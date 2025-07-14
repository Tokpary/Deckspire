using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects.EnemyActions
{
    [CreateAssetMenu(fileName = "DiscardRandomCard", menuName = "ScriptableObjects/Entity/EnemyAction/DiscardRandomCard", order = 1)]
    public class DiscardRandomCard : EnemyActionSO
    {
        public override void ExecuteAction(Enemy enemy, GameContext gameContext)
        {
            int index = Random.Range(0, gameContext.allCardsInHand.Count);
            gameContext.allCardsInHand[index].Discard();
        }
    }
}