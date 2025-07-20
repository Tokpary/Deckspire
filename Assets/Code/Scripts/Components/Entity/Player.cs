using System;
using Code.Scripts.Components.Card.ScriptableObjects;
using Code.Scripts.Components.GameBoard;
using Code.Scripts.Components.GameManagment;
using Code.Scripts.Components.GameManagment.GameStates;
using Code.Scripts.Components.Handdeck;
using UnityEngine;

namespace Code.Scripts.Components.Entity
{
    public class Player : Entity
    {
        public HandDeckManager HandDeck { get; set; }

        private void Awake()
        {
            HandDeck = GetComponentInChildren<HandDeckManager>();
        }

        public void Initialize(GameRulesData gameRulesData)
        {
            MaxHealth = gameRulesData.PlayerMaxHealth;
            MaxEnergy = gameRulesData.PlayerMaxMana;
            
            GameManager.Instance.UIManager.UpdateTurn(MaxHealth);
            CurrentHealth = MaxHealth;
            CurrentEnergy = MaxEnergy;
        }

        public override void Die()
        {
            GameManager.Instance.GameFlowManager.SetState(new DialogueState(GameManager.Instance.GameFlowManager, "PlayerDeath"));
        }

        public virtual void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            GameManager.Instance.UIManager.UpdateTurn(CurrentHealth);
        }
    }
}