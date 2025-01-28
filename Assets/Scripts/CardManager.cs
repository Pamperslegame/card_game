using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Card
{
    public string cardName;
    public int damage;
    public int health;
}

[System.Serializable]
public class CardCategories
{
    public List<Card> robot;
    public List<Card> nightmare;
    public List<Card> light;
    public List<Card> spider;
}

public class CardManager : MonoBehaviour
{
    public CardCategories cardCategories;

    private string jsonFilePath;
    void Start()
    {
        jsonFilePath = Path.Combine(Application.dataPath, "Data/cards.json");

        LoadCardsFromJSON();
    }

    void LoadCardsFromJSON()
    {
        if (File.Exists(jsonFilePath))
        {
            string jsonContent = File.ReadAllText(jsonFilePath);

            cardCategories = JsonUtility.FromJson<CardCategories>(jsonContent);

            DisplayCardsInConsole();
        }
        else
        {
            Debug.LogError($"Fichier JSON introuvable : {jsonFilePath}");
        }
    }

    void DisplayCardsInConsole()
    {
        Debug.Log("Catégorie : Robot");
        foreach (var card in cardCategories.robot)
        {
            Debug.Log($"Carte : {card.cardName}, Dégâts : {card.damage}, Vie : {card.health}");
        }

        Debug.Log("Catégorie : Nightmare");
        foreach (var card in cardCategories.nightmare)
        {
            Debug.Log($"Carte : {card.cardName}, Dégâts : {card.damage}, Vie : {card.health}");
        }

        Debug.Log("Catégorie : Light");
        foreach (var card in cardCategories.light)
        {
            Debug.Log($"Carte : {card.cardName}, Dégâts : {card.damage}, Vie : {card.health}");
        }

        Debug.Log("Catégorie : Spider");
        foreach (var card in cardCategories.spider)
        {
            Debug.Log($"Carte : {card.cardName}, Dégâts : {card.damage}, Vie : {card.health}");
        }
    }
    void Update()
    {

    }
}
