using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects
{
    public enum TriggerTiming {
        OnPlay,
        OnDeath,
        OnTurnStart,
        OnTurnEnd,
        Passive
    }

    public abstract class CardAbilitySo : ScriptableObject
    {
        public TriggerTiming triggerTiming;
        public abstract void Activate(ACard card, GameContext gameContext);
    }
}