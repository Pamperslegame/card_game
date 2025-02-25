using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform parentToReturnTo;
    private bool isOnBoard = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Début du glissement : OnBeginDrag appelé");
        startPosition = transform.position;
        parentToReturnTo = transform.parent;
        transform.SetParent(transform.root); // Détache la carte de son parent
        GetComponent<CanvasGroup>().blocksRaycasts = false; // Désactive les raycasts
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Glissement en cours : OnDrag appelé");
        transform.position = Input.mousePosition; // Déplace la carte avec la souris
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Fin du glissement : OnEndDrag appelé");
        if (!isOnBoard)
        {
            Debug.Log("La carte n'est pas sur le plateau, retour à la position initiale");
            transform.position = startPosition;
            transform.SetParent(parentToReturnTo);
        }
        else
        {
            Debug.Log("La carte est sur le plateau, elle reste en place");
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true; // Réactive les raycasts
    }

    public void SetOnBoard(bool value)
    {
        Debug.Log("SetOnBoard appelé avec la valeur : " + value);
        isOnBoard = value;
    }

    // Fonction pour gérer le clic
    public void OnCardClicked()
    {
        Debug.Log("La carte a été cliquée !");
        // Ajoutez ici des actions spécifiques au clic
    }
}