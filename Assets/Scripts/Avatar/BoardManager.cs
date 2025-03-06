using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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


    private void ApplyRobotSynergies()
    {
        int robotCount = placedCards.Count(card => card.CardDefinition.SynergieType == SynergieType.Robot);

        Debug.Log($"Nombre de robots sur le plateau : {robotCount}");

        int bonusDamage = 0;

        // Calcul du bonus de synergie en fonction du nombre de robots
        if (robotCount == 2)
        {
            bonusDamage = 1;
        }
        else if (robotCount == 3)
        {
            bonusDamage = 2;
        }
        else if (robotCount == 4)
        {
            bonusDamage = 4;
        }
        else if (robotCount == 1) // Cas avec une seule carte, bonus -1 (par exemple)
        {
            bonusDamage = -1;
        }

        // Appliquer la synergie sur chaque carte
        foreach (Card card in placedCards)
        {
            if (card.CardDefinition.SynergieType == SynergieType.Robot)
            {
                Debug.Log($"Carte {card.name} avant application de la synergie, Dégâts : {card.CardDefinition.Damage}");
                card.ApplyTemporaryDamageBuff(bonusDamage); // Appliquer le buff avec le bon bonus
            }
            else
            {
                card.ResetToInitialDamageStats();
            }
        }
    }

    private void ApplyLightSynergies()
    {
        int lightCount = placedCards.Count(card => card.CardDefinition.SynergieType == SynergieType.Chevalier);

        Debug.Log($"Nombre de lumières sur le plateau : {lightCount}");

        int bonusHealth = 0;

        if (lightCount == 2)
        {
            bonusHealth = 3;  
        }
        else if (lightCount == 3)
        {
            bonusHealth = 5; 
        }
        else if (lightCount == 4)
        {
            bonusHealth = 10; 
        }
        else if (lightCount == 1) 
        {
            bonusHealth = 2;  
        }

        foreach (Card card in placedCards)
        {
            if (card.CardDefinition.SynergieType == SynergieType.Chevalier)
            {
                Debug.Log($"Carte {card.name} avant application de la synergie, PV : {card.CardDefinition.MaxHealth}");
                card.ApplyTemporaryHealthBuff(bonusHealth); 
            }
            else
            {
                card.ResetToInitialHealthStats();
            }
        }
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

            ApplyRobotSynergies();
            ApplyLightSynergies();
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

            ApplyRobotSynergies();
            ApplyLightSynergies();
        }
    }
}
