using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Pour utiliser Image
using TMPro;
using System.Collections.Generic;

// Ajoutez IPointerClickHandler à la liste des interfaces implémentées
public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public CardDefinition CardDefinition;  
    public GameObject Cadre;                
    public GameObject Image;                

    public TextMeshProUGUI damageText;
    public TextMeshProUGUI hpText;

    private Image cadreImage;   
    private Image cardImage;    
    private Vector3 startPosition;
    private Transform parentToReturnTo; 
    private bool isOnBoard = false;

    private int initialDamage;
    private int initialHealth;

    private bool isReviveActive = false;
    private bool isBlockActive = false;
    private bool isDoubleStrikeActive = false;

    private bool isDead = false;

    public static Card SelectedCard = null; 
    public bool IsSelected = false;  

    void Start()
    {
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

            initialDamage = CardDefinition.Damage;
            initialHealth = CardDefinition.MaxHealth;
            
            if (cadreImage != null)
                cadreImage.sprite = CardDefinition.CadreImage;

            if (cardImage != null)
                cardImage.sprite = CardDefinition.CardImage;

            if (damageText != null)
            {
                damageText.text = $"{CardDefinition.Damage}"; 
                Debug.Log($"Dégâts de la carte {gameObject.name} : {damageText.text}");
            }

            if (hpText != null)
                hpText.text = $"{CardDefinition.MaxHealth}";

        }
        else
        {
            Debug.LogWarning("Aucune CardDefinition assignée à la carte.");
        }
    }

    public void ApplyTemporaryDamageBuff(int bonusDamage)
    {
        int newDamage = initialDamage + bonusDamage;
        if (damageText != null)
        {
            damageText.text = $"{newDamage}"; 
        }
    }

    public void ApplyTemporaryHealthBuff(int bonusHealth)
    {
        int newHealth = initialHealth + bonusHealth;
        if (hpText != null)
        {
            hpText.text = $"{newHealth}"; 
        }
    }

    public void ResetToInitialDamageStats()
    {
        if (damageText != null)
        {
            damageText.text = $"{initialDamage}";  
        }
    }

    public void ResetToInitialHealthStats() {
        if (hpText != null) 
        {
            hpText.text = $"{initialHealth}";
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
        Debug.Log($"Clic détecté sur {gameObject.name}, bouton : {eventData.button}");

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            HandleLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            HandleRightClick();
        }
    }

    public void HandleLeftClick()
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

    private void HandleRightClick()
    {
        if (IsMouseOverCard()) 
        {
            Debug.Log($"Clic droit détecté sur {gameObject.name}");
            TryPlaceOnBoard();
        }
    }


    private bool IsMouseOverCard()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            if (result.gameObject == gameObject)
            {
                return true;
            }
        }
        return false;
    }

    private void TryPlaceOnBoard()
    {
        BoardManager boardManager = FindObjectOfType<BoardManager>();
        Character player = FindObjectOfType<Character>();

        if (boardManager == null)
        {
            Debug.LogError("BoardManager non trouvé !");
            return;
        }

        if (player == null)
        {
            Debug.LogError("Character (joueur) non trouvé !");
            return;
        }

        Debug.Log($"Joueur Gold : {player.Gold()}, Coût de la carte : {CardDefinition.Cost}");
        Debug.Log($"Peut placer carte ? {boardManager.CanPlaceCard()}");

        if (player.Gold() >= CardDefinition.Cost && boardManager.CanPlaceCard())
        {
            player.SpendGold(CardDefinition.Cost);
            boardManager.PlaceCard(this);
            Debug.Log("✅ Carte placée sur le plateau !");
        }
        else
        {
            Debug.Log("⛔ Pas assez de gold ou plateau plein !");
        }
    }

    private void CheckDeath()
    {
        int currentHP = int.Parse(hpText.text);
        if (currentHP <= 0)
        {
            BoardManager boardManager = FindObjectOfType<BoardManager>();
            if (boardManager != null)
            {
                boardManager.RemoveCard(this);
            }
        }
    }

    private void Attack(Card targetCard)
    {
        if (targetCard != null && targetCard.hpText != null)
        {
            int targetHP = int.Parse(targetCard.hpText.text);
            targetHP -= CardDefinition.Damage;
            targetHP = Mathf.Max(targetHP, 0);
            targetCard.hpText.text = targetHP.ToString();

            Debug.Log($"Carte {gameObject.name} attaque ! Dégâts : {CardDefinition.Damage}, HP de la cible : {targetHP}");

            targetCard.CheckDeath(); 
        }
    }

    public void ActivateRevive(bool isActive)
    {
        isReviveActive = isActive;
    }

    public void ActivateBlock(bool isActive)
    {
        isBlockActive = isActive;
    }

    public void ActivateDoubleStrike(bool isActive)
    {
        isDoubleStrikeActive = isActive;
    }

    public void TakeDamage(int damage)
    {
        if (isBlockActive)
        {
            Debug.Log($"{name} bloque l'attaque ! Aucun dégât n'est subi.");
            isBlockActive = false;  
            return;
        }

        if (!isDead)
        {
            CardDefinition.MaxHealth -= damage;
            Debug.Log($"{name} subit {damage} dégâts. PV restants : {CardDefinition.MaxHealth}");
            
            if (CardDefinition.MaxHealth <= 0)
            {
                if (isReviveActive)
                {
                    Revive();
                }
                else
                {
                    Die();
                }
            }
        }
    }

    private void Revive()
    {
        isDead = false;
        CardDefinition.MaxHealth = 1;  
        Debug.Log($"{name} revient à la vie avec {CardDefinition.MaxHealth} PV !");
    }

    private void Die()
    {
        isDead = true;
        Debug.Log($"{name} est morte !");
    }
}
