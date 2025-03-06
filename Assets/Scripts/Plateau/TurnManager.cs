using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class TurnManager : MonoBehaviour
{
    public TMP_Text turnText;
    public Button endTurnButton;

    public TMP_Text titre;  // Affiche le titre du passif
    public TMP_Text description;  // Affiche la description du passif

    private int currentTurn = 1;
    private int currentPlayer = 1;

    private List<PassiveEffect> passives = new List<PassiveEffect>
    {
        new ReviveEffect(),
        new BlockEffect(),
        new DoubleStrikeEffect()
    };

    private PassiveEffect activePassive = null;

    // Références aux 4 joueurs
    public Character player1;
    public Character player2;
    public Character player3;
    public Character player4;

    void Start()
    {
        if (turnText == null || endTurnButton == null || titre == null || description == null)
        {
            Debug.LogError("Références manquantes dans l'inspecteur !");
            return;
        }

        if (player1 == null || player2 == null || player3 == null || player4 == null)
        {
            Debug.LogError("Références aux joueurs manquantes !");
            return;
        }

        UpdateTurnUI();
        endTurnButton.onClick.AddListener(EndTurn);
    }

    public void EndTurn()
    {
        currentTurn++;
        currentPlayer = (currentPlayer % 4) + 1;  // Passer au joueur suivant (de 1 à 4)

        ApplyRandomPassive();
        DistributeGolds();  // Distribuer les golds à la fin du tour
        UpdateTurnUI();
        Debug.Log($"Tour terminé. Tour actuel: {currentTurn}, Joueur actuel: {currentPlayer}, Passif actif: {activePassive?.Title ?? "Aucun"}");
    }

    void UpdateTurnUI()
    {
        turnText.text = $"Tour {currentTurn} - Joueur {currentPlayer}";
    }

    void ApplyRandomPassive()
    {
        List<PassiveEffect> selectedPassives = new List<PassiveEffect>();

        // Vérifier les 10% de chance pour chaque passif
        foreach (PassiveEffect passive in passives)
        {
            if (Random.value <= 0.1f) 
            {
                selectedPassives.Add(passive);
            }
        }

        if (selectedPassives.Count > 0)
        {
            // S'il y a plusieurs passifs sélectionnés, on en prend un au hasard
            activePassive = selectedPassives[Random.Range(0, selectedPassives.Count)];
        }
        else
        {
            activePassive = null;
        }

        UpdateBoardUI();
    }

    void UpdateBoardUI()
    {
        if (activePassive != null)
        {
            titre.text = activePassive.Title;
            description.text = activePassive.Description;
        }
        else
        {
            titre.text = "Passif";
            description.text = "Aucun passif ce tour";
        }
    }

    void DistributeGolds()
    {
        if (player1 == null || player2 == null || player3 == null || player4 == null) return;

        int player1HP = player1.GetCurrentHP(); 
        int player2HP = player2.GetCurrentHP();
        int player3HP = player3.GetCurrentHP();
        int player4HP = player4.GetCurrentHP();

        List<(Character player, int hp)> players = new List<(Character, int)>
        {
            (player1, player1HP),
            (player2, player2HP),
            (player3, player3HP),
            (player4, player4HP)
        };

        players = players.OrderByDescending(p => p.hp).ToList();

    
        if (players[0].hp == players[1].hp && players[1].hp == players[2].hp && players[2].hp == players[3].hp)
        {
            
            foreach (var player in players)
            {
                player.player.EarnGold(5);
            }
            Debug.Log("Égalité totale ! Chaque joueur gagne 5 golds.");
        }
        else
        {
            
            players[0].player.EarnGold(10); 
            players[1].player.EarnGold(8);  
            players[2].player.EarnGold(6);  
            players[3].player.EarnGold(5);  

            Debug.Log("Golds distribués : Premier : 10, Deuxième : 8, Troisième : 6, Quatrième : 5");
        }
    }
}
