namespace DefaultNamespace;

public enum TriggerTiming {
    OnPlay,
    OnDeath,
    OnTurnStart,
    OnTurnEnd,
    Passive
}

public abstract class CardAbilitySO : ScriptableObject
{
    public TriggerTiming triggerTiming;
    public abstract void Activate(ACard card, GameContext gameContext);
}