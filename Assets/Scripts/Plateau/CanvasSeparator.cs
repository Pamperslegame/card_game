using UnityEngine;

public class CanvasSeparator : MonoBehaviour
{
    public RectTransform topCanvasRectTransform;   // Référence au RectTransform du Canvas supérieur
    public RectTransform bottomCanvasRectTransform; // Référence au RectTransform du Canvas inférieur

    void Start()
    {
        // Mettre à jour la taille et la position des Canvases
        UpdateCanvasPositions();
    }

    void UpdateCanvasPositions()
    {
        // Récupérer la largeur et la hauteur de l'écran
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Canvas supérieur : moitié supérieure de l'écran
        topCanvasRectTransform.anchorMin = new Vector2(0, 0.5f);
        topCanvasRectTransform.anchorMax = new Vector2(1, 1);
        topCanvasRectTransform.sizeDelta = new Vector2(screenWidth, screenHeight / 2);
        topCanvasRectTransform.anchoredPosition = new Vector2(0, screenHeight / 4); // Centrer verticalement dans la moitié supérieure

        // Canvas inférieur : moitié inférieure de l'écran
        bottomCanvasRectTransform.anchorMin = new Vector2(0, 0);
        bottomCanvasRectTransform.anchorMax = new Vector2(1, 0.5f);
        bottomCanvasRectTransform.sizeDelta = new Vector2(screenWidth, screenHeight / 2);
        bottomCanvasRectTransform.anchoredPosition = new Vector2(0, -screenHeight / 4); // Centrer verticalement dans la moitié inférieure
    }
}
