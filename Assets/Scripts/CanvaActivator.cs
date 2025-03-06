using UnityEngine;

public class CanvaActivator : MonoBehaviour
{
    public Canvas[] canvases; 

    void Start()
    {
        int playerCount = PlayerPrefs.GetInt("PlayerCount", 2);
        
        if (canvases.Length < 4)
        {
            Debug.LogError("Assurez-vous d'assigner les 4 canvases dans l'inspecteur.");
            return;
        }

        for (int i = playerCount; i < canvases.Length; i++)
        {
            canvases[i].gameObject.SetActive(false);
        }
    }
}
