using System;
using UnityEngine;

namespace Code.Scripts.Components.Entity.ScriptableObjects
{
    public abstract class DieEventSO : ScriptableObject
    {
        public abstract void OnDieEvent(Action onComplete = null);
    }
}