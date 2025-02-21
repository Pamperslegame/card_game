using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public int playerID;
    public int health;
    public int maxHealth;
    public int xp;
    public int golds;
    public int damage;
    public List<Card> hand = new List<Card>();

    [SerializeField] private CharacterProfile characterProfile; 

    public void InitializePlayer(string name, int id)
    {
        playerName = name;
        playerID = id;

        if (characterProfile != null)
        {
            health = characterProfile.BaseHp;
            maxHealth = characterProfile.BaseHp; 
            xp = characterProfile.BaseXp;
            golds = characterProfile.BaseGolds;
            damage = characterProfile.BaseDamage;
        }
        else
        {
            Debug.LogError("Aucun CharacterProfile assigné !");
        }
    }

    public void AddCardToHand(Card card)
    {
        hand.Add(card);
    }

    public void PlayCard(Card card)
    {
        hand.Remove(card);
    }

    public void DisplayPlayerInfo()
    {
        Debug.Log($"Player: {playerName}, Health: {health}/{maxHealth}, XP: {xp}, Golds: {golds}, Cards in Hand: {hand.Count}");
    }

    public void TakeDamage(int damageAmount)
    {
        health = Mathf.Max(0, health - damageAmount);
        DisplayPlayerInfo(); 
    }

    public void Heal(int healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
        DisplayPlayerInfo(); 
    }

    public void AddExperience(int xpAmount)
    {
        xp += xpAmount;
        DisplayPlayerInfo(); 
    }

    public void AddGold(int goldAmount)
    {
        golds += goldAmount;
        DisplayPlayerInfo(); 
    }
}
