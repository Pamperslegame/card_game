using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public TMP_Text turnText;      
    public Button endTurnButton; 

    private int currentTurn = 1;
    private int currentPlayer = 1;

    void Start()
    {
        UpdateTurnUI();
        endTurnButton.onClick.AddListener(EndTurn);
    }

    public void EndTurn()
    {
        currentTurn++;                        
        currentPlayer = 3 - currentPlayer;
        UpdateTurnUI();
    }

    void UpdateTurnUI()
    {
        turnText.text = $"Tour {currentTurn} - Joueur {currentPlayer}";
    }
}
