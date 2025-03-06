using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] private Transform handSlots; 
    [SerializeField] private CardRoller cardRoller; 
    [SerializeField] private GameObject cardPrefab; 
    [SerializeField] private BoardManager boardManager; 

    private List<Card> currentHand = new List<Card>();

    void Start()
    {
        DrawHand(); 
    }

    public void DrawHand()
    { 
        List<CardDefinition> newHand = cardRoller.RollHand();
        
        if (newHand == null) 
        {
            Debug.Log("Aucune main tirée (manque de golds).");
            return;
        }
        ClearHand();
        foreach (CardDefinition cardDef in newHand)
        {
            GameObject newCardObject = Instantiate(cardPrefab, handSlots);
            Card newCard = newCardObject.GetComponent<Card>();
            newCard.CardDefinition = cardDef;
            newCard.InitializeCard();

            currentHand.Add(newCard); 
        }

    }

    private void ClearHand()
    {
        foreach (Card card in currentHand)
        {
            Destroy(card.gameObject);
        }
        currentHand.Clear();
    }

}