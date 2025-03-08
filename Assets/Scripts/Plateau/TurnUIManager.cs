using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnUIManager : MonoBehaviour
{
    public TMP_Text turnText;
    public Button endTurnButton;
    public TMP_Text titre;
    public TMP_Text description;
    public TMP_Text[] playerTimerTexts;  // Texte pour chaque joueur

    private void Start()
    {
        if (turnText == null || endTurnButton == null || titre == null || description == null || playerTimerTexts == null || playerTimerTexts.Length != 4)
        {
            Debug.LogError("Références UI manquantes !");
            return;
        }

        endTurnButton.onClick.AddListener(() => TurnManager.Instance.EndTurn());
        TurnManager.Instance.OnTurnChanged += UpdateUI;
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Afficher le tour actuel et le joueur actif
        turnText.text = $"Tour {TurnManager.Instance.CurrentTurn} - Joueur {TurnManager.Instance.CurrentPlayer}";

        // Afficher les timers pour chaque joueur
        for (int i = 0; i < TurnManager.Instance.playerTimers.Length; i++)
        {
            playerTimerTexts[i].text = Mathf.Ceil(TurnManager.Instance.playerTimers[i].GetTimeRemaining()).ToString() + "s";
        }

        // Afficher les détails du passif actif
        if (TurnManager.Instance.ActivePassive != null)
        {
            titre.text = TurnManager.Instance.ActivePassive.Title;
            description.text = TurnManager.Instance.ActivePassive.Description;
        }
        else
        {
            titre.text = "Passif";
            description.text = "Aucun passif ce tour";
        }
    }
}
