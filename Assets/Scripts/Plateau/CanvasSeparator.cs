using UnityEngine;

public class CanvasSeparator : MonoBehaviour
{
    public RectTransform topCanvasRectTransform;
    public RectTransform bottomCanvasRectTransform;

    void Start()
    {
        UpdateCanvasPositions();
    }

    void UpdateCanvasPositions()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        topCanvasRectTransform.anchorMin = new Vector2(0, 0.5f);
        topCanvasRectTransform.anchorMax = new Vector2(1, 1);
        topCanvasRectTransform.sizeDelta = new Vector2(screenWidth, screenHeight / 2);
        topCanvasRectTransform.anchoredPosition = new Vector2(0, screenHeight / 4);

        bottomCanvasRectTransform.anchorMin = new Vector2(0, 0);
        bottomCanvasRectTransform.anchorMax = new Vector2(1, 0.5f);
        bottomCanvasRectTransform.sizeDelta = new Vector2(screenWidth, screenHeight / 2);
        bottomCanvasRectTransform.anchoredPosition = new Vector2(0, -screenHeight / 4); 
    }
}
