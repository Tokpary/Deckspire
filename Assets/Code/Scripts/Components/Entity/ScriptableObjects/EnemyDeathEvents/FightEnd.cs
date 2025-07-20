using System;
using Code.Scripts.Components.Card.ScriptableObjects;
using Code.Scripts.Components.GameManagment;
using Code.Scripts.Components.GameManagment.GameStates;
using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects.EnemyDeathEvents
{ 
    [CreateAssetMenu(fileName = "EndFight", menuName = "ScriptableObjects/Entity/DeathEvents/EndFight", order = 1)]
    public class FightEnd : DieEventSO
    {
        [SerializeField] private CardSO[] cardsToAdd;
        public override void OnDieEvent(Action onComplete = null)
        {
            GameManager.Instance.GameFlowManager.SetState(new DialogueState(GameManager.Instance.GameFlowManager, "FoolBeated"));
            GameManager.Instance.GameBoard.AddCardToCurrentFullDeck(cardsToAdd);
            onComplete?.Invoke();
        }
    }
}