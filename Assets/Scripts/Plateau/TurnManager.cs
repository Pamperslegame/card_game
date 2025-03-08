using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public int CurrentTurn { get; private set; } = 1;
    public int CurrentPlayer { get; private set; } = 1;

    public TurnTimer[] playerTimers;
    public GameObject[] playerUI;
    public Button endTurnButton;

    private int[] playerOrder = new int[4];
    private int[] attackOrder;
    private int currentAttackerIndex = 0;
    private bool isCombatPhase = false;
    private int currentTargetIndex = -1;

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
        if (!isCombatPhase)
        {
            int playerIndex = playerOrder[CurrentPlayer - 1] - 1;
            playerTimers[playerIndex].StopTimer();

            Debug.Log("Joueur " + playerOrder[CurrentPlayer - 1] + " a fini son tour.");

            CurrentPlayer++;

            if (CurrentPlayer > 4)
            {
                EndPreparationPhase();
            }
            else
            {
                StartNextPlayerPreparation();
            }

            ApplyRandomPassive();
            OnTurnChanged?.Invoke();
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(EndAttackPhase());
        }
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
        isCombatPhase = true;
        SetupCombatOrder();
        StartCombatPhase();
    }

    private void SetupCombatOrder()
    {
        attackOrder = playerOrder.OrderBy(x => UnityEngine.Random.value).ToArray();
        currentAttackerIndex = 0;
    }

    private void StartCombatPhase()
    {
        if (currentAttackerIndex >= attackOrder.Length)
        {
            EndCombatPhase();
            return;
        }

        int attackerIndex = attackOrder[currentAttackerIndex] - 1;
        Debug.Log("Joueur " + attackOrder[currentAttackerIndex] + " commence l'attaque.");

        attackSelectionImage.SetActive(true);
        currentTargetIndex = -1;

        for (int i = 0; i < targetButtons.Length; i++)
        {
            targetButtons[i].gameObject.SetActive(i != attackerIndex);
        }

        for (int i = 0; i < playerUI.Length; i++)
        {
            playerUI[i].SetActive(i == attackerIndex);
        }

        // Activation du bouton "Fin de tour"
        endTurnButton.gameObject.SetActive(true);
    }

    public void SelectTarget(int targetPlayerIndex)
    {
        if (targetPlayerIndex == attackOrder[currentAttackerIndex] - 1) return;

        attackSelectionImage.SetActive(false);
        currentTargetIndex = targetPlayerIndex;

        Debug.Log($"Attaquant: {attackOrder[currentAttackerIndex]}, Cible sélectionnée: {targetPlayerIndex}");

        for (int i = 0; i < playerUI.Length; i++)
        {
            playerUI[i].SetActive(i == attackOrder[currentAttackerIndex] - 1 || i == targetPlayerIndex);
        }

        StartCombatTimer();
    }

    private void StartCombatTimer()
    {
        int attackerIndex = attackOrder[currentAttackerIndex] - 1;
        playerTimers[attackerIndex].StartTimer();

        Debug.Log($"Démarrage du timer pour le joueur {attackOrder[currentAttackerIndex]}");
    }

    private IEnumerator EndAttackPhase()
    {
        yield return new WaitForSeconds(30f);

        int attackerIndex = attackOrder[currentAttackerIndex] - 1;
        playerTimers[attackerIndex].StopTimer();

        Debug.Log("Attaque terminée, passage au prochain attaquant");
        currentAttackerIndex++;

        StartCombatPhase();
    }

    private void EndCombatPhase()
    {
        Debug.Log("Phase de combat terminée !");
        isCombatPhase = false;
        
        // Désactivation du bouton "Fin de tour"
        endTurnButton.gameObject.SetActive(false);

        CurrentTurn++;
        StartPreparationPhase();
    }
}
