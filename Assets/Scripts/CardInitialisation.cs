using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public CardDefinition CardDefinition;
    public GameObject Cadre;
    public GameObject Image;

    private SpriteRenderer cadreRenderer;
    private SpriteRenderer imageRenderer;
    private Vector3 startPosition;
    private Transform parentToReturnTo; 
    private bool isOnBoard = false;

    void Start()
    {
        if (Cadre != null) cadreRenderer = Cadre.GetComponent<SpriteRenderer>();
        if (Image != null) imageRenderer = Image.GetComponent<SpriteRenderer>();

        InitializeCard();
    }

    private void InitializeCard()
    {
        if (CardDefinition != null)
        {
            if (cadreRenderer != null)
            {
                cadreRenderer.sprite = CardDefinition.CadreImage;
            }
            else
            {
                Debug.LogWarning("SpriteRenderer du cadre non trouvé !");
            }

            if (imageRenderer != null)
            {
                imageRenderer.sprite = CardDefinition.CardImage;
            }
            else
            {
                Debug.LogWarning("SpriteRenderer de l'image non trouvé !");
            }

            ApplySynergy();
        }
        else
        {
            Debug.LogWarning("Aucune définition de carte assignée !");
        }
    }

    private void ApplySynergy()
    {
        if (CardDefinition.Synergie != null)
        {
            CardDefinition.Synergie.ApplySynergyEffect(CardDefinition, this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        startPosition = transform.position;
        parentToReturnTo = transform.parent;
        transform.SetParent(transform.root); 
        GetComponent<CanvasGroup>().blocksRaycasts = false; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isOnBoard)
        {
            transform.position = startPosition; 
            transform.SetParent(parentToReturnTo); 
        }

        GetComponent<CanvasGroup>().blocksRaycasts = true; 
    }

    public void SetOnBoard(bool value)
    {
        isOnBoard = value;
    }
}