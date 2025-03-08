using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseNumberOfPlayers : MonoBehaviour
{
    public void SetPlayerCount(int count)
    {
        // Sauvegarder le nombre de joueurs dans PlayerPrefs
        PlayerPrefs.SetInt("PlayerCount", count);
        PlayerPrefs.Save();

        // Charger la scène suivante (choisir pseudo et avatar)
        SceneManager.LoadScene("ChoosePlayers");
    }
}
