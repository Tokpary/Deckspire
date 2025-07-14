using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Components.Handdeck;
using Code.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] CardSO[] cardPrefabs; 
    Stack<CardSO> deck = new Stack<CardSO>();
    [SerializeField] GameObject cardPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (CardSO card in cardPrefabs)
        {
            deck.Push(card);
        }
        ShuffleDeck(deck);

        for (int i = 0; i < HandDeckManager.Instance.MaxCardsInHand - 1; i++)
        {
            AddCardToHand(false);
        }
        AddCardToHand(); 
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
        AddCardToHand();
    }

    private void AddCardToHand(bool deploy = true)
    {
        CardSO drawnCard = deck.Count > 0 ? deck.Pop() : null;
        
        if (drawnCard != null)
        {
            GameObject cardObject = Instantiate(cardPrefab, HandDeckManager.Instance.transform);
            ACard cardComponent = cardObject.GetComponent<ACard>();
            cardComponent.SetSortingOrder(100); 
            HandDeckManager.Instance.AddCard(cardComponent, deploy);
        }
    }
}
