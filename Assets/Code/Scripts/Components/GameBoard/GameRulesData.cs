
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
        
        public bool IsModifyingCard { get; set; }
        // BOSS RULES
        public int DecoyPerRound { get; set; }
        public bool DrawOnEmptyHandOnly { get; set; }
        public bool DecoyOnTableCards { get; set; }
        public bool LifeLossOn2CardsDecoyed { get; set; }
        public bool CardLifeTimeIsVisible { get; set; } = true; // Default value, can be changed in the inspector
        
        public bool IsSkippingNextEnemy { get; set; }
        public bool IsCleanseApplied { get; set; }
        

        public ACard SelectedCard { get; set; }
        public int NumberOfRefillsToWin { get; set; }
        public bool IsDeathWinCondition { get; set; }
        
        public bool NextRoundIsEnergyLoss { get; set; } = false; // Default value, can be changed in the inspector
        public bool NextRoundIsFreeze { get; set; }
        public bool IsHermitWinCondition { get; set; } = false; // Default value, can be changed in the inspector

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
            IsModifyingCard = DefaultGameRules.IsModifyingCard;
            DecoyPerRound = DefaultGameRules.DecoyPerRound;
            DrawOnEmptyHandOnly = DefaultGameRules.DrawOnEmptyHandOnly;
            DecoyOnTableCards = DefaultGameRules.DecoyOnTableCards;
            IsSkippingNextEnemy = DefaultGameRules.IsSkippingNextEnemy;
            IsCleanseApplied = DefaultGameRules.IsCleanseApplied;
            LifeLossOn2CardsDecoyed = DefaultGameRules.LifeLossOn2CardsDecoyed;
            NumberOfRefillsToWin = DefaultGameRules.NumberOfRefillsToWin;
            CardLifeTimeIsVisible = DefaultGameRules.CardLifeTimeIsVisible;
            IsDeathWinCondition = DefaultGameRules.IsDeathWinCondition;
            IsHermitWinCondition = DefaultGameRules.IsHermitWinCondition;
        }

        public void UpdateEnemyRules(GameRulesSO enemyEnemyRulesData)
        {
            if (enemyEnemyRulesData == null) return;

            DrawOnEmptyHandOnly = enemyEnemyRulesData.DrawOnEmptyHandOnly;
            DecoyPerRound = enemyEnemyRulesData.DecoyPerRound;
            DecoyOnTableCards = enemyEnemyRulesData.DecoyOnTableCards;
            LifeLossOn2CardsDecoyed = enemyEnemyRulesData.LifeLossOn2CardsDecoyed;
            NumberOfRefillsToWin = enemyEnemyRulesData.NumberOfRefillsToWin;
            CardLifeTimeIsVisible = enemyEnemyRulesData.CardLifeTimeIsVisible;
            IsDeathWinCondition = enemyEnemyRulesData.IsDeathWinCondition;
            IsHermitWinCondition = enemyEnemyRulesData.IsHermitWinCondition;
        }
    }
}