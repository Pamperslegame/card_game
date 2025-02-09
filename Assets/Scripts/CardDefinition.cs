using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card System/Card")]
public class CardDefinition : ScriptableObject
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int damage;
    [SerializeField] private int cost;
    [SerializeField] private Sprite cardImage;

    public int Health => health;
    public int MaxHealth => maxHealth;
    public int Damage => damage;
    public int Cost => cost;
    public Sprite CardImage => cardImage;

    public void Initialize(int health, int maxHealth, int damage, int cost, Sprite image)
    {
        this.health = health;
        this.maxHealth = maxHealth;
        this.damage = damage;
        this.cost = cost;
        this.cardImage = image;
    }
}
