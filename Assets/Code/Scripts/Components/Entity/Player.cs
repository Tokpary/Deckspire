using System;
using Code.Scripts.Components.Card.ScriptableObjects;
using Code.Scripts.Components.GameBoard;
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
            CurrentHealth = MaxHealth;
            CurrentEnergy = MaxEnergy;
        }

        public override void Die()
        {
            Debug.Log("Player has died.");
        }
    }
}