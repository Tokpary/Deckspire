using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Components.Card.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/Card/Card", order = 1)]
    public class CardSO : ScriptableObject   
    {
        public string cardName;
        public string description;
        public Sprite cardImage;
        public Sprite cardBackground;
        public int manaCost;
        public int lifetime;
        public int cardType; // 0: Unit, 1: Spell, 2: Rule
        
        public List<CardAbilitySo> abilities;
    }
}