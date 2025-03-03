using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterProfile characterProfile;
    public GameObject avatar;

    private int currentHp;
    private int currentXp;
    private int currentGolds;
    private int lvl;

    public TextMeshProUGUI lvlText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI goldText;
    public Slider xpSlider; // Barre d'XP
    public int Level => lvl;
    public int Gold => currentGolds;

    private Image avatarsprite;

    // Table des XP requis par niveau
    private int[] xpRequiredPerLevel = { 0, 6, 12, 20 };

    private void Start()
    {
        if (characterProfile != null && characterProfile.IsValid())
        {
            currentHp = characterProfile.BaseHp;
            currentXp = characterProfile.BaseXp;
            currentGolds = characterProfile.BaseGolds; // ✅ Correction ici
            lvl = 1;

            UpdateXPBar();
            UpdateUI();
            
            Debug.Log("Character initialized with " + currentGolds + " golds.");
        }
        else
        {
            Debug.LogError("CharacterProfile is invalid or missing!");
        }
        
        if (avatar != null) avatarsprite = avatar.GetComponent<Image>();
    }


    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
        UpdateUI();
    }

    public void EarnGold(int gold)
    {
        currentGolds += gold;
        UpdateUI();
    }

    public void SpendGoldForXP()
    {
        Debug.Log(currentGolds.ToString());
        if (currentGolds >= 2) // Vérifie si le joueur a assez de golds
        {
            currentGolds -= 2;
            GainXp(2);
            UpdateUI();
        }
        else
        {
            Debug.Log("Pas assez de golds !");
        }
    }

    public void GainXp(int xp)
    {
        currentXp += xp;
        CheckLevelUp();
        UpdateUI();
    }

    public bool SpendGold(int amount)
    {
        if (currentGolds >= amount)
        {
            currentGolds -= amount;
            UpdateUI();
            return true; // Dépense réussie
        }

        Debug.Log("Pas assez de golds !");
        return false; // Dépense échouée
    }


    private void CheckLevelUp()
    {
        while (lvl < xpRequiredPerLevel.Length && currentXp >= xpRequiredPerLevel[lvl])
        {
            currentXp -= xpRequiredPerLevel[lvl]; // Soustrait l'XP requis
            lvl++; // Monte de niveau
            Debug.Log("Nouveau niveau atteint : " + lvl);
        }

        UpdateXPBar();
    }

    private void UpdateXPBar()
    {
        if (xpSlider != null && lvl < xpRequiredPerLevel.Length)
        {
            int xpNeeded = xpRequiredPerLevel[lvl];
            xpSlider.maxValue = xpNeeded;
            xpSlider.value = currentXp;
        }
    }

    private void Die()
    {
        Debug.Log(characterProfile.CharacterName + " has died!");
        // Ajouter ici une logique de respawn ou de game over
    }

    public string GetCharacterInfo()
    {
        return characterProfile.GetCharacterStats() + ", Current HP: " + currentHp + ", Current XP: " + currentXp + ", Current Golds: " + currentGolds;
    }

    private void UpdateUI()
    {
        if (lvlText) lvlText.text = lvl.ToString();
        if (hpText) hpText.text = currentHp.ToString();
        if (goldText) goldText.text = currentGolds.ToString();
        if (avatarsprite) avatarsprite.sprite = characterProfile.Avatar;
    }
}
