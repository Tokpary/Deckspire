using System;
using Code.Scripts.Components.GameManagment;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Scripts.Components.Entity.ScriptableObjects.EnemyActions
{
    [CreateAssetMenu(fileName = "IncrementRandomCardEnergy", menuName = "ScriptableObjects/Entity/EnemyAction/IncrementRandomCardEnergy", order = 1)]
    public class IncrementRandomCardEnergy : EnemyActionSO
    {
        public int IncrementAmount = 1;
        public override void ExecuteAction(Action onComplete = null)
        {
            int index = Random.Range(0, GameManager.Instance.GameBoard.PlayerHand.Count);
            ACard card = GameManager.Instance.GameBoard.PlayerHand[index];
            
            card.EnergyCost += IncrementAmount;
            card.UpdateCard();
            onComplete?.Invoke();
        }
    }
}