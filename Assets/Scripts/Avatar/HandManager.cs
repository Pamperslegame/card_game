using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] private Transform handSlots; // Le "Content" de la ScrollView
    [SerializeField] private CardRoller cardRoller; // Référence au CardRoller
    [SerializeField] private GameObject cardPrefab; // Prefab de la carte

    private List<Card> currentHand = new List<Card>();

    void Start()
    {
        DrawHand(); // Tirer une main au début
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

            currentHand.Add(newCard); // Stocker la carte en main
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
