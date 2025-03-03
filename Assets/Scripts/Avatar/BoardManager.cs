using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Transform boardSlots;
    [SerializeField] private Transform cimetery;
    [SerializeField] private Vector3 boardCardScale = new Vector3(0.8f, 0.8f, 1f);
    [SerializeField] private Vector3 cimeteryCardScale = new Vector3(0.36f, 1f, 1f);

    private List<Card> placedCards = new List<Card>();

    void Start()
    {
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        placedCards.Clear();
        foreach (Transform child in boardSlots)
        {
            Card card = child.GetComponent<Card>();
            if (card != null)
            {
                placedCards.Add(card);
                card.SetOnBoard(true);
            }
        }

        Debug.Log($"Cartes déjà sur le plateau : {placedCards.Count}");
    }

    public bool CanPlaceCard()
    {
        return placedCards.Count < 4;
    }

    public void PlaceCard(Card card)
    {
        if (CanPlaceCard())
        {
            card.transform.SetParent(boardSlots);
            card.transform.localScale = boardCardScale; 
            card.SetOnBoard(true);
            placedCards.Add(card);
        }
    }

    public void RemoveCard(Card card)
    {
        if (placedCards.Contains(card))
        {
            placedCards.Remove(card);
            card.transform.SetParent(cimetery); 
            card.transform.localScale = cimeteryCardScale; 
            Debug.Log($"Carte {card.name} envoyée au cimetière.");
        }
    }
}
