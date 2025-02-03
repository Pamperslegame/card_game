using UnityEngine;

public class ShowHidePanel : MonoBehaviour
{
    public GameObject panel;


    public void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
    }
}
