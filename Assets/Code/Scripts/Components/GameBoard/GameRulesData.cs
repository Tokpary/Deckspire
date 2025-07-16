namespace DefaultNamespace;

public class GameRulesData
{
    [SerializeField] private GameRulesSO DefaultGameRules;
    
    public int PlayerHealth;
    public int PlayerMana;
    public int PlayerMaxMana;
    public int PlayerMaxHealth;
    public int MaxHandSize;
        
    // GAME RULES
    public bool IsReinformentApplied;
    public bool IsNecromancyApplied;
    public bool IsHandGameApplied;
    public bool IsMoonLightApplied;
    public bool IsOptionsApplied;
    public bool IsRushApplied;
    public bool IsFriendlyFireApplied;
    public int IsMirrorApplied;

    // BOSS RULES
    public int DecoyPerRound;
    public bool DrawOnEmptyHandOnly;
    public bool DecoyOnTableCards;

    public bool IsSkippingNextEnemy;

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