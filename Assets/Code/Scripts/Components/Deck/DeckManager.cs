using System.Collections;
using System.Collections.Generic;
using Code.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckManager : MonoBehaviour, IPointerClickHandler
{
    Stack<CardSO> deck = new Stack<CardSO>();
    
    // Start is called before the first frame update
    void Start()
    {
        ShuffleDeck(deck);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void ShuffleDeck(Stack<CardSO> deck)
    {
        List<CardSO> deckList = new List<CardSO>(deck);
        for (int i = 0; i < deckList.Count; i++)
        {
            int j = Random.Range(i, deckList.Count);
            (deckList[i], deckList[j]) = (deckList[j], deckList[i]);
        }
        deck.Clear();
        foreach (var card in deckList)
        {
            deck.Push(card);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CardSO drawnCard = deck.Count > 0 ? deck.Pop() : null;
        Debug.Log(drawnCard.cardName + " drawn from deck." +
                  (drawnCard != null ? $" Mana Cost: {drawnCard.manaCost}, Lifetime: {drawnCard.lifetime}" : " No cards left in deck."));
    }
}
