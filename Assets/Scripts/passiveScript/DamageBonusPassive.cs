using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageBonusPassive", menuName = "Card System/Passives/DamageBonus")]
public class DamageBonusPassive : ScriptableObject, IPassiveEffect
{
    [SerializeField] private int damageBonus;

    public void Apply(CardDefinition card)
    {
        card.ApplyDamageBonus(damageBonus);
    }
}
