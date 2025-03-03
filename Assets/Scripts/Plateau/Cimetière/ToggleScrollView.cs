using UnityEngine;

public class ToggleScrollView : MonoBehaviour
{
    public GameObject targetObject; 

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
