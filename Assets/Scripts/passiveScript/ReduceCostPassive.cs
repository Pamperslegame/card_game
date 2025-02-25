using UnityEngine;

[CreateAssetMenu(fileName = "ReduceCostPassive", menuName = "PassiveEffects/ReduceCost")]
public class ReduceCostPassive : ScriptableObject, IPassiveEffect
{
    [SerializeField] private string title = "Réduction de coût";
    [SerializeField] private string description = "Réduit le coût de la carte.";
    [SerializeField] private PassiveRarity rarity = PassiveRarity.Common;
    [SerializeField] private int costReductionAmount = 2;

    public string Title => title;
    public string Description => description;
    public PassiveRarity Rarity => rarity;

    public void Initialize(int reductionAmount)
    {
        costReductionAmount = reductionAmount;
    }

    public void Apply(CardDefinition card)
    {
        if (card != null)
        {
            card.Cost = Mathf.Max(0, card.Cost - costReductionAmount);
            Debug.Log($"Coût réduit de {costReductionAmount}. Nouveau coût : {card.Cost}");
        }
    }
}