using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandManager : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab; 
    [SerializeField] private Transform handContainer; 
    [SerializeField] private List<CardDefinition> deck = new List<CardDefinition>(); 

    private List<CardDefinition> hand; 
    public int handSize = 5; 

    private void Start()
    {
        hand = new List<CardDefinition>();
        UpdateHand();
    }

    public void UpdateHand()
    {
        foreach (Transform child in handContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < handSize; i++)
        {
            if (deck.Count > 0)
            {
                CardDefinition drawnCard = deck[Random.Range(0, deck.Count)];
                hand.Add(drawnCard);
                deck.Remove(drawnCard);

                GameObject cardObject = Instantiate(cardPrefab, handContainer);
                CardUI cardUI = cardObject.GetComponent<CardUI>(); 
                cardUI.InitializeCard(drawnCard); 
            }
        }
    }

    public void NextTurn()
    {
        UpdateHand();
    }
}
