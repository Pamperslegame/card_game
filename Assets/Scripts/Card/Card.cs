using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Pour utiliser Image
using TMPro;

// Ajoutez IPointerClickHandler à la liste des interfaces implémentées
public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public CardDefinition CardDefinition;  
    public GameObject Cadre;                // Cadre de la carte
    public GameObject Image;                // Image de la carte

    public TextMeshProUGUI damageText;
    public TextMeshProUGUI hpText;

    private Image cadreImage;   // UI Image au lieu de SpriteRenderer
    private Image cardImage;    // UI Image au lieu de SpriteRenderer
    private Vector3 startPosition;
    private Transform parentToReturnTo; 
    private bool isOnBoard = false;

    // Ajout pour gérer la sélection des cartes et l'attaque
    public static Card SelectedCard = null; // Carte actuellement sélectionnée
    public bool IsSelected = false;  // Si cette carte est sélectionnée

    void Start()
    {
        // Récupérer les composants UI Image
        if (Cadre != null) cadreImage = Cadre.GetComponent<Image>();
        if (Image != null) cardImage = Image.GetComponent<Image>();

        if (damageText == null || hpText == null)
        {
            Debug.LogError("TextMeshProUGUI pour Damage et HP non affecté !");
        }

        InitializeCard();
    }

    public void InitializeCard()
    {
        if (CardDefinition != null)
        {
            // Modifier les images avec les bons sprites
            if (cadreImage != null)
                cadreImage.sprite = CardDefinition.CadreImage;

            if (cardImage != null)
                cardImage.sprite = CardDefinition.CardImage;

            // Mettre à jour les valeurs de texte
            if (damageText != null)
                damageText.text = $"{CardDefinition.Damage}";

            if (hpText != null)
                hpText.text = $"{CardDefinition.MaxHealth}";

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CardDefinition != null)
        {
            if (SelectedCard == null)
            {
                SelectedCard = this;
                IsSelected = true;
                Debug.Log("Carte sélectionnée : " + gameObject.name);
            }
            else
            {
                if (SelectedCard != this)
                {
                    // Vérifier si les cartes appartiennent à des parents différents
                    if (SelectedCard.transform.parent != this.transform.parent)
                    {
                        // Lancer l'attaque : appliquer les dégâts
                        Attack(SelectedCard);
                    }
                    else
                    {
                        Debug.Log("Les cartes sont dans le même groupe, elles ne peuvent pas s'attaquer.");
                    }

                    // Réinitialiser la sélection après l'attaque
                    SelectedCard = null;
                    IsSelected = false;
                }
            }
        }
        else
        {
            Debug.LogWarning("Aucune CardDefinition assignée à la carte cliquée.");
        }
    }

    private void Attack(Card targetCard)
    {
        // Réduire les points de vie de la carte cible
        if (targetCard != null && targetCard.hpText != null)
        {
            int targetHP = int.Parse(targetCard.hpText.text);
            targetHP -= CardDefinition.Damage; // Appliquer les dégâts

            // S'assurer que les points de vie ne deviennent pas négatifs
            targetHP = Mathf.Max(targetHP, 0);

            targetCard.hpText.text = targetHP.ToString(); // Mettre à jour les points de vie

            Debug.Log($"Carte {gameObject.name} attaque ! Dégâts : {CardDefinition.Damage}, HP de la cible : {targetHP}");
        }
    }
}
