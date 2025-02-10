using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card System/Card")]
public class CardDefinition : ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private int cost;
    [SerializeField] public Sprite cardImage;
    [SerializeField] public Sprite cadreImage;

    public int Health => health;
    public int MaxHealth => maxHealth;
    public int Damage => damage;
    public int Cost => cost;
    public Sprite CardImage => cardImage;
    public Sprite CadreImage => cadreImage;

    public void Initialize(int health, int maxHealth, int damage, int cost, Sprite image, Sprite cadre)
    {
        this.health = health;
        this.maxHealth = maxHealth;
        this.damage = damage;
        this.cost = cost;
        this.cardImage = image;
        this.cadreImage = cadre;
    }
}
