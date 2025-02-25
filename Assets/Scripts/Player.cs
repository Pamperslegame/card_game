using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public string playerName;
    public int playerID;
    public List<Card> hand = new List<Card>();

    [SerializeField] private CharacterProfile characterProfile;
    [SerializeField] private SpriteRenderer avatarRenderer;

    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private TextMeshProUGUI lvlText;

    private int health;
    private int maxHealth;
    private int xp;
    private int golds;

    public int Health => health;
    public int MaxHealth => maxHealth;
    public int Xp => xp;
    public int Golds => golds;

    public event System.Action OnStatsChanged;

    public void InitializePlayer(string name, int id)
    {
        playerName = name;
        playerID = id;

        if (characterProfile != null)
        {
            if (avatarRenderer != null)
            {
                avatarRenderer.sprite = characterProfile.Avatar;
            }

            health = characterProfile.BaseHp;
            maxHealth = characterProfile.BaseHp;
            xp = characterProfile.BaseXp;
            golds = characterProfile.BaseGolds;
        }
        else
        {
            Debug.LogError("Aucun CharacterProfile assigné !");
        }

        UpdateUI();
    }

    public void AddCardToHand(Card card)
    {
        hand.Add(card);
    }

    public void PlayCard(Card card)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card);
        }
        else
        {
            Debug.LogWarning("La carte n'est pas dans la main du joueur !");
        }
    }

    public void DisplayPlayerInfo()
    {
        Debug.Log($"Player: {playerName}, Health: {health}/{maxHealth}, XP: {xp}, Golds: {golds}, Cards in Hand: {hand.Count}");
    }

    public void TakeDamage(int damageAmount)
    {
        health = Mathf.Max(0, health - damageAmount);
        DisplayPlayerInfo();
        UpdateUI();
    }

    public void Heal(int healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
        DisplayPlayerInfo();
        UpdateUI();
    }

    public void AddExperience(int xpAmount)
    {
        xp += xpAmount;
        DisplayPlayerInfo();
        UpdateUI();
    }

    public void AddGold(int goldAmount)
    {
        golds += goldAmount;
        DisplayPlayerInfo();
        UpdateUI();
    }

    private void UpdateUI()
    {
        Debug.Log("UpdateUI appelée !");

        if (hpText != null)
        {
            hpText.text = $"{health}/{maxHealth}";
        }
        else
        {
            Debug.LogWarning("hpText non assigné !");
        }

        if (goldText != null)
        {
            goldText.text = $"{golds}";
        }
        else
        {
            Debug.LogWarning("goldText non assigné !");
        }

        if (lvlText != null)
        {
            lvlText.text = $"XP: {xp}";
        }
        else
        {
            Debug.LogWarning("lvlText non assigné !");
        }
    }
}