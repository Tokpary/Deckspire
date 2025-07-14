using Code.Scripts.Components.Card.ScriptableObjects;
using Code.Scripts.Components.Handdeck;
using UnityEngine;

namespace Code.Scripts.Components.Entity
{
    public class Player : Entity
    {
        public HandDeckManager HandDeck { get; set; }

        [SerializeField] public CardSO[] CurrentFullDeck;
        private void Awake()
        {
            HandDeck = GetComponentInChildren<HandDeckManager>();
        }
    }
}