using System.Collections.Generic;
using UnityEngine;

public class MatchupManager : MonoBehaviour
{
    private List<string> matchups = new List<string>(); // Liste des duels
    private int currentRound = 0; // Indice de la manche actuelle

    public void InitializeMatchups(int playerCount)
    {
        matchups = GenerateMatchups(playerCount); // Utilisation de ta fonction existante
        Debug.Log("Matchups générés !");
    }

    public List<string> GenerateMatchups(int playerCount)
    {
        List<string> matchups = new List<string>();
        List<string> playerNames = new List<string>();

        // Récupérer les noms des joueurs depuis PlayerPrefs
        for (int i = 0; i < playerCount; i++)
        {
            playerNames.Add(PlayerPrefs.GetString($"Player{i + 1}Name"));
        }

        // Générer les duels
        for (int i = 0; i < playerCount; i++)
        {
            for (int j = i + 1; j < playerCount; j++)
            {
                matchups.Add($"{playerNames[i]} vs {playerNames[j]}");
            }
        }

        Debug.Log("Liste des Matchups :");
        foreach (string matchup in matchups)
        {
            Debug.Log(matchup);
        }

        return matchups;
    }

    public void StartNextMatch()
    {
        if (currentRound < matchups.Count)
        {
            Debug.Log($"🔴 Manche {currentRound + 1} : {matchups[currentRound]}");
            currentRound++;
        }
        else
        {
            Debug.Log("✅ Toutes les manches ont été jouées !");
        }
    }
}
