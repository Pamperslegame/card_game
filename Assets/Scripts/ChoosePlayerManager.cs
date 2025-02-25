using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePlayerManager : MonoBehaviour
{
    public void OnChoosePlayers(int playerCount)
    {
        Debug.Log($"Nombre de joueurs choisis : {playerCount}");

        if (GameManager.Instance != null)
        {
            Debug.Log("GameManager existe, initialisation des joueurs...");
            GameManager.Instance.InitializePlayers(playerCount);
            
            Debug.Log("Chargement de la scène Plateau...");
            SceneManager.LoadScene("Plateau");
        }
        else
        {
            Debug.LogError("GameManager n'est pas initialisé !");
        }
    }

}
