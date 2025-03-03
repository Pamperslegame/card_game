using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CardRoller : MonoBehaviour
{
    [SerializeField] private Character character; // Référence au joueur pour son niveau
    [SerializeField] private List<CardDefinition> allCards; // Toutes les cartes disponibles

    private Dictionary<string, List<CardDefinition>> cardPools = new Dictionary<string, List<CardDefinition>>();

    private void Start()
    {
        // Trier les cartes en fonction de leur rareté (nom de cadreImage)
        cardPools["Common"] = allCards.Where(c => c.CadreImage.name == "Common").ToList();
        cardPools["Rare"] = allCards.Where(c => c.CadreImage.name == "Rare").ToList();
        cardPools["Epic"] = allCards.Where(c => c.CadreImage.name == "Epic").ToList();
        cardPools["Legendary"] = allCards.Where(c => c.CadreImage.name == "Legendary").ToList();
    }

    public CardDefinition RollCard()
    {
        int level = character.Level; // Obtenir le niveau du joueur

        // Probabilités en fonction du niveau (modifiable)
        Dictionary<string, float> probabilities = new Dictionary<string, float>
        {
            { "Common", level == 1 ? 0.7f : level == 2 ? 0.55f : 0.3f },
            { "Rare", level == 1 ? 0.2f : level == 2 ? 0.3f : 0.4f },
            { "Epic", level == 1 ? 0.05f : level == 2 ? 0.1f : 0.2f },
            { "Legendary", level == 1 ? 0.0f : level == 2 ? 0.05f : 0.1f }
        };


        string selectedRarity = SelectRarity(probabilities);
        if (cardPools[selectedRarity].Count > 0)
        {
            return cardPools[selectedRarity][Random.Range(0, cardPools[selectedRarity].Count)];
        }

        return null; // Si aucune carte trouvée (erreur potentielle)
    }

    private string SelectRarity(Dictionary<string, float> probabilities)
    {
        float roll = Random.value;
        float cumulative = 0f;

        foreach (var entry in probabilities)
        {
            cumulative += entry.Value;
            if (roll <= cumulative)
            {
                return entry.Key;
            }
        }
        return "Common"; // Sécurité (au cas où)
    }

    public List<CardDefinition> RollHand()
    {
        if (!character.SpendGold(2)) // Vérifie si on peut dépenser 2 golds
        {
            Debug.Log("Pas assez de golds pour tirer une main !");
            return null; // Annule le tirage si pas assez de golds
        }

        List<CardDefinition> hand = new List<CardDefinition>();

        for (int i = 0; i < 4; i++)
        {
            CardDefinition newCard = RollCard();
            if (newCard != null)
            {
                hand.Add(newCard);
            }
        }

        return hand;
    }

}
