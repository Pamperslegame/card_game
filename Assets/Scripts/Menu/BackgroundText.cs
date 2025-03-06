using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackgroundText : MonoBehaviour
{
    public Image backgroundImage;
    public TMP_Text textElement;

    void Start()
    {
        if (backgroundImage != null && textElement != null)
        {
            backgroundImage.rectTransform.sizeDelta = textElement.rectTransform.sizeDelta + new Vector2(20, 10);
        }
    }
}
