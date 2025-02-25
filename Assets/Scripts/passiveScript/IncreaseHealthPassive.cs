using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseHealthPassive", menuName = "PassiveEffects/IncreaseHealth")]
public class IncreaseHealthPassive : ScriptableObject, IPassiveEffect
{
    [SerializeField] private string title = "Health augmentation";
    [SerializeField] private string description = "Increase the health of the card of 3.";
    [SerializeField] private PassiveRarity rarity = PassiveRarity.Common;
    [SerializeField] private int healthIncreaseAmount = 3;

    public string Title => title;
    public string Description => description;
    public PassiveRarity Rarity => rarity;
    public void Initialize(int increaseAmount)
    {
        healthIncreaseAmount = increaseAmount;
    }

    public void Apply(CardDefinition card)
    {
        if (card != null)
        {
            card.MaxHealth += healthIncreaseAmount;
            Debug.Log($"Santé augmentée de {healthIncreaseAmount}. Nouvelle santé : {card.MaxHealth}");
        }
    }
}