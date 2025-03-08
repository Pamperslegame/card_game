using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public int CurrentTurn { get; private set; } = 1;
    public int CurrentPlayer { get; private set; } = 1;

    public TurnTimer[] playerTimers;
    public GameObject[] playerUI;
    public Button endTurnButton;

    private int[] playerOrder = new int[4];
    public Character[] players;
    public GameObject attackSelectionImage;
    public Button[] targetButtons; 

    public event System.Action OnTurnChanged;

    public PassiveEffect ActivePassive { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        players = FindObjectsOfType<Character>();

        for (int i = 0; i < playerTimers.Length; i++)
        {
            playerTimers[i].SetTimerDuration(30f);
            playerTimers[i].OnTimerEnd += HandleTurnEnd;
        }

        foreach (var ui in playerUI)
        {
            ui.SetActive(false);
        }

        endTurnButton.onClick.AddListener(EndTurn);

        SetupTargetButtons();

        ShufflePlayerOrder();
        StartPreparationPhase();
    }

    private void SetupTargetButtons()
    {
        for (int i = 0; i < targetButtons.Length; i++)
        {
            int targetIndex = i; 
            targetButtons[i].onClick.AddListener(() => SelectTarget(targetIndex));
        }
    }

    private void ShufflePlayerOrder()
    {
        playerOrder = Enumerable.Range(1, 4).OrderBy(x => UnityEngine.Random.value).ToArray();
        Debug.Log("Ordre des joueurs après tirage: " + string.Join(", ", playerOrder));
    }

    private void StartPreparationPhase()
    {
        Debug.Log("Début de la phase de préparation");
        StartNextPlayerPreparation();
    }

    private void StartNextPlayerPreparation()
    {
        if (CurrentPlayer > 4)
        {
            EndPreparationPhase();
            return;
        }

        int playerIndex = playerOrder[CurrentPlayer - 1] - 1;

        Debug.Log("Tour " + CurrentTurn + " - Joueur " + playerOrder[CurrentPlayer - 1] + " commence la préparation.");

        playerTimers[playerIndex].StartTimer();
        playerUI[playerIndex].SetActive(true);

        for (int i = 0; i < playerUI.Length; i++)
        {
            if (i != playerIndex)
            {
                playerUI[i].SetActive(false);
            }
        }
    }

    public void EndTurn()
    {
        int playerIndex = playerOrder[CurrentPlayer - 1] - 1;
        playerTimers[playerIndex].StopTimer();

        Debug.Log("Joueur " + playerOrder[CurrentPlayer - 1] + " a fini son tour.");

        CurrentPlayer++;

        if (CurrentPlayer > 4)
        {
            EndPreparationPhase();
            CurrentPlayer = 1;
            CurrentTurn++;
            Debug.Log("Passage au tour " + CurrentTurn);
            StartPreparationPhase();
        }
        else
        {
            StartNextPlayerPreparation();
        }

        ApplyRandomPassive();
        OnTurnChanged?.Invoke();
    }

    private void HandleTurnEnd()
    {
        EndTurn();
    }

    private void ApplyRandomPassive()
    {
        PassiveEffect[] passifs = new PassiveEffect[] {
            new ReviveEffect(),
            new BlockEffect(),
            new DoubleStrikeEffect()
        };

        if (UnityEngine.Random.value <= 0.1f)
        {
            ActivePassive = passifs[UnityEngine.Random.Range(0, passifs.Length)];
            Debug.Log("Passif appliqué : " + ActivePassive.GetType().Name);
        }
        else
        {
            ActivePassive = null;
            Debug.Log("Aucun passif appliqué.");
        }
    }

    private void EndPreparationPhase()
    {
        Debug.Log("Phase de préparation terminée !");
        StartCombatPhase();
    }

    private void StartCombatPhase()
    {
        Debug.Log("Début de la phase de combat");
        int attackerIndex = DetermineAttacker();
        Debug.Log("Joueur " + playerOrder[attackerIndex] + " commence l'attaque.");

        attackSelectionImage.SetActive(true);

        for (int i = 0; i < targetButtons.Length; i++)
        {
            targetButtons[i].gameObject.SetActive(i != attackerIndex);
        }

        for (int i = 0; i < playerUI.Length; i++)
        {
            playerUI[i].SetActive(i == attackerIndex);
        }
    }

    private int DetermineAttacker()
    {
        int[] sortedPlayers = playerOrder.OrderBy(p => GetPlayerStats(p)).ToArray();
        return Array.IndexOf(playerOrder, sortedPlayers[0]);
    }

    private int GetPlayerStats(int playerId)
    {
        Character player = players[playerId - 1];
        return player.GetCurrentHP() - player.Gold();
    }

    public void SelectTarget(int targetPlayerIndex)
    {
        attackSelectionImage.SetActive(false);

        Debug.Log("Attaquant: " + (CurrentPlayer - 1) + " cible Joueur: " + targetPlayerIndex);

        for (int i = 0; i < playerUI.Length; i++)
        {
            playerUI[i].SetActive(i == (CurrentPlayer - 1) || i == targetPlayerIndex);
        }

        playerTimers[CurrentPlayer - 1].StartTimer();
        playerTimers[targetPlayerIndex].StartTimer();
    }
}
