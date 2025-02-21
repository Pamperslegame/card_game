using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthBonusPassive", menuName = "Card System/Passives/HealthBonus")]
public class HealthBonusPassive : ScriptableObject, IPassiveEffect
{
    [SerializeField] private int healthBonus;

    public void Apply(CardDefinition card)
    {
        card.ApplyHealthBonus(healthBonus);
    }
}
