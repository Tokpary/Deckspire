using UnityEngine;

namespace Code.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewCard", menuName = "ScriptableObjects/Card", order = 1)]
    public class CardSO : ScriptableObject   
    {
        public string cardName;
        public string description;
        public Sprite cardImage;
        public int manaCost;
        public int lifetime;
        
        public List<CardAbilitySO> abilities;
    }
}