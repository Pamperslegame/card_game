using UnityEngine;

[CreateAssetMenu(fileName = "newCard", menuName = "Card System/Card")]
public class CardDefinition : ScriptableObject
{
    [Header("Card Stats")]
    public int health;
    public int damage;
    public int cost;

    [Header("Card Info")]
    public string cardName;
    public string passive;

    [Header("Image")]
    public Sprite cadre;
    public Sprite picture;

    public bool IsDead()
    {
        return health <= 0;
    }
}
