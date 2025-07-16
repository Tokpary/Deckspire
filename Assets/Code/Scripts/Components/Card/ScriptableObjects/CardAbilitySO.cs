using Code.Scripts.Components.GameManagment;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects
{

    public abstract class CardAbilitySo : ScriptableObject
    {
        public abstract void Activate(ACard card);
        public abstract void Deactivate(ACard card);
    }
}