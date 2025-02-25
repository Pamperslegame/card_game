using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<Player> players = new List<Player>();
    public List<List<Player>> matchups = new List<List<Player>>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("Une autre instance de GameManager existe déjà !");
            Destroy(gameObject);
        }
    }

    public void InitializePlayers(int playerCount)
    {
        players.Clear();

        for (int i = 1; i <= playerCount; i++)
        {
            Player newPlayer = new Player(); 
            newPlayer.InitializePlayer($"Player {i}", i); 
            players.Add(newPlayer); 
        }

        Debug.Log($"Initialisation de {playerCount} joueurs.");

        CreateMatchups();
    }

    private void CreateMatchups()
    {
        matchups.Clear(); 

        for (int i = 0; i < players.Count; i++)
        {
            for (int j = i + 1; j < players.Count; j++)
            {
                List<Player> matchup = new List<Player> { players[i], players[j] };
                matchups.Add(matchup); 
            }
        }

        foreach (var matchup in matchups)
        {
            Debug.Log($"Match: {matchup[0].playerName} vs {matchup[1].playerName}");
        }
    }
}
