using UnityEngine;

namespace Code.Scripts.Components.GameBoard.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameRules", menuName = "ScriptableObjects/GameRules", order = 1)]
    public class GameRulesSO : ScriptableObject
    {
        // PLAYER
        public int PlayerHealth;
        public int PlayerMana;
        public int MaxHandSize;
        public int PlayerMaxMana;
        public int PlayerMaxHealth;
        
        
        // GAME RULES
        public bool IsReinformentApplied;
        public bool IsNecromancyApplied;
        public bool IsHandGameApplied;
        public bool IsMoonLightApplied;
        public bool IsOptionsApplied;
        public bool IsRushApplied;
        public bool IsFriendlyFireApplied;
        public bool IsSkippingNextEnemy;
        public bool IsMirrorApplied;

        public bool IsModifyingCard;

        // BOSS RULES
        public int DecoyPerRound;
        public bool DrawOnEmptyHandOnly;
        public bool DecoyOnTableCards;
    }
}