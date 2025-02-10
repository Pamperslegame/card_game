using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card System/Card")]
public class CardDefinition : ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private int cost;
    [SerializeField] private Sprite cardImage;
    [SerializeField] private Sprite cadreImage;
    [SerializeField] private SynergieType synergieType;
    [SerializeField] private Synergie synergie; 

    public int Health => health;
    public int MaxHealth => maxHealth;
    public int Damage => damage;
    public int Cost => cost;
    public Sprite CardImage => cardImage;
    public Sprite CadreImage => cadreImage;
    public SynergieType SynergieType => synergieType;
    public Synergie Synergie => synergie; 

    public void Initialize(int health, int maxHealth, int damage, int cost, Sprite image, Sprite cadre, Synergie synergie)
    {
        this.health = health;
        this.maxHealth = maxHealth;
        this.damage = damage;
        this.cost = cost;
        this.cardImage = image;
        this.cadreImage = cadre;
        this.synergie = synergie; 
    }

    public void ApplyPassiveEffect(IPassiveEffect passiveEffect)
    {
        passiveEffect?.Apply(this);
    }

    public void ApplyDamageBonus(int bonusAmount)
    {
        damage += bonusAmount;
    }

    public void ApplyHealthBonus(int bonusAmount)
    {
        health += bonusAmount;
    }
}
