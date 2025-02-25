using UnityEngine;

public class ToggleVisibility : MonoBehaviour
{
    public GameObject targetObject; // Peut être n'importe quel GameObject

    public void ToggleObjectVisibility()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(!targetObject.activeSelf);
            Debug.Log(targetObject.name + " toggled to " + targetObject.activeSelf);
        }
        else
        {
            Debug.LogWarning("Aucun objet assigné à 'targetObject' !");
        }
    }
}
