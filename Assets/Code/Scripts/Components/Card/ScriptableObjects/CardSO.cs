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
        public int manaCost;
        public int lifetime;
        
        public List<CardAbilitySo> abilities;
    }
}