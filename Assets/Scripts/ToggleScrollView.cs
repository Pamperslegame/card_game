using UnityEngine;

public class ToggleScrollView : MonoBehaviour
{
    public GameObject scrollView;

    public void ToggleScrollViewVisibility()
    {
        Debug.Log("ToggleScrollViewVisibility appelée !"); // Vérifiez que cette méthode est bien appelée

        if (scrollView != null)
        {
            Debug.Log("ScrollView trouvée !"); // Vérifiez que la ScrollView est bien assignée
            scrollView.SetActive(!scrollView.activeSelf);
        }
        else
        {
            Debug.LogWarning("ScrollView non assignée !");
        }
    }
}