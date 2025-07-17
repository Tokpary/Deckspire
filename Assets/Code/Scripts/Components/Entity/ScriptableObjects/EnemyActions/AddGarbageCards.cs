using System;
using Code.Scripts.Components.Card.ScriptableObjects;
using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects.EnemyActions
{
    
    [CreateAssetMenu(fileName = "AddCardToPlayerDeck", menuName = "ScriptableObjects/Entity/EnemyAction/AddCardToPlayerDeck", order = 1)]
    public class AddGarbageCards : EnemyActionSO
    {
        [SerializeField] private CardSO CardPrefab;
        public override void ExecuteAction(Action onComplete = null)
        {
           GameManager.Instance.GameBoard.AddCardToDiscardStackFromEnemy(CardPrefab, () =>
            {
                onComplete?.Invoke();
            });
        }
    }
}