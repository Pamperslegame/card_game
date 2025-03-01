using UnityEngine;
using UnityEngine.SceneManagement;

public class UniversalSceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Debug.Log("Scene load: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
