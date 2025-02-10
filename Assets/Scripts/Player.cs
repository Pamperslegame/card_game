using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public int playerID;
    public List<Card> hand = new List<Card>();
    public int health = 20;

    public void InitializePlayer(string name, int id)
    {
        playerName = name;
        playerID = id;
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
        Debug.Log($"Player: {playerName}, Health: {health}, Cards in Hand: {hand.Count}");
    }
}
