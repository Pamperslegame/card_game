using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseDamagePassive", menuName = "PassiveEffects/IncreaseDamage")]
public class IncreaseDamagePassive : ScriptableObject, IPassiveEffect
{
    [SerializeField] private string title = "Augmenter les dégâts";
    [SerializeField] private string description = "Augmente les dégâts de la carte.";
    [SerializeField] private PassiveRarity rarity = PassiveRarity.Common;
    [SerializeField] private int damageIncreaseAmount = 5;

    public string Title => title;
    public string Description => description;
    public PassiveRarity Rarity => rarity;

    public void Initialize(int bonusAmount)
    {
        damageIncreaseAmount = bonusAmount;
    }

    public void Apply(CardDefinition card)
    {
        if (card != null)
        {
            card.Damage += damageIncreaseAmount;
            Debug.Log($"Dégâts augmentés de {damageIncreaseAmount}. Nouveaux dégâts : {card.Damage}");
        }
    }
}