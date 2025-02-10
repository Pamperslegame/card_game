using UnityEngine;

[CreateAssetMenu(fileName = "BonusDamageEffect", menuName = "Passives/Effects/Bonus Damage")]
public class BonusDamageEffect : ScriptableObject, IPassiveEffect {
    public int bonusAmount = 2;  

    public void Apply(CardDefinition card) {
        card.ApplyDamageBonus(bonusAmount);  
    }
}
