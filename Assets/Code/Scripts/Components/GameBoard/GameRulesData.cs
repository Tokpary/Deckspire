
using System;
using Code.Scripts.Components.GameBoard.ScriptableObjects;
using UnityEngine;

namespace Code.Scripts.Components.GameBoard
{
    public class GameRulesData : MonoBehaviour
    {
        [SerializeField] private GameRulesSO DefaultGameRules;

        public int PlayerHealth { get; set; }
        public int PlayerMana { get; set; }
        public int PlayerMaxMana { get; set; }
        public int PlayerMaxHealth { get; set; }
        public int MaxHandSize { get; set; }
        
        // GAME RULES
        public bool IsReinformentApplied { get; set; }
        public bool IsNecromancyApplied { get; set; }
        public bool IsHandGameApplied { get; set; }
        public bool IsMoonLightApplied { get; set; }
        public bool IsOptionsApplied { get; set; }
        public bool IsRushApplied { get; set; }
        public bool IsFriendlyFireApplied { get; set; }
        public bool IsMirrorApplied { get; set; }

        // BOSS RULES
        public int DecoyPerRound { get; set; }
        public bool DrawOnEmptyHandOnly { get; set; }
        public bool DecoyOnTableCards { get; set; }
        public bool IsSkippingNextEnemy { get; set; }

        private void Awake()
        {
            InitializeGameRules();
        }

        public void InitializeGameRules()
        {
            PlayerHealth = DefaultGameRules.PlayerHealth;
            PlayerMana = DefaultGameRules.PlayerMana;
            PlayerMaxHealth = DefaultGameRules.PlayerMaxHealth;
            PlayerMaxMana = DefaultGameRules.PlayerMaxMana;
            MaxHandSize = DefaultGameRules.MaxHandSize;
            IsReinformentApplied = DefaultGameRules.IsReinformentApplied;
            IsNecromancyApplied = DefaultGameRules.IsNecromancyApplied;
            IsHandGameApplied = DefaultGameRules.IsHandGameApplied;
            IsMoonLightApplied = DefaultGameRules.IsMoonLightApplied;
            IsOptionsApplied = DefaultGameRules.IsOptionsApplied;
            IsRushApplied = DefaultGameRules.IsRushApplied;
            IsFriendlyFireApplied = DefaultGameRules.IsFriendlyFireApplied;
            IsMirrorApplied = DefaultGameRules.IsMirrorApplied;
            DecoyPerRound = DefaultGameRules.DecoyPerRound;
            DrawOnEmptyHandOnly = DefaultGameRules.DrawOnEmptyHandOnly;
            DecoyOnTableCards = DefaultGameRules.DecoyOnTableCards;
            IsSkippingNextEnemy = DefaultGameRules.IsSkippingNextEnemy;
        }
    }
}