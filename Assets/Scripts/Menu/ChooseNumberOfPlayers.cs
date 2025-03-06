using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseNumberOfPlayers : MonoBehaviour
{
    public void SetPlayerCount(int count)
    {
        PlayerPrefs.SetInt("PlayerCount", count);
        PlayerPrefs.Save();

        SceneManager.LoadScene("ChoosePlayers");
    }
}
