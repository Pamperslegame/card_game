using UnityEngine;

public class Synergie : ScriptableObject
{
    [SerializeField] private SynergieType synergieType;
    [SerializeField] private int effectAmount;

    public SynergieType SynergieType => synergieType;
    public int EffectAmount => effectAmount;

    public void ApplySynergyEffect(CardDefinition cardDefinition, Card card)
    {
        IPassiveEffect synergyEffect = GetSynergyEffect();
        if (synergyEffect != null)
        {
            synergyEffect.Apply(cardDefinition);
        }
    }

    private IPassiveEffect GetSynergyEffect()
    {
        switch (synergieType)
        {
            case SynergieType.Chevalier:
                return CreateDamageBonusEffect();
            case SynergieType.Araignee:
                return CreateCostReductionEffect();
            default:
                return null;
        }
    }

    private IPassiveEffect CreateDamageBonusEffect()
    {
        var effect = ScriptableObject.CreateInstance<IncreaseDamagePassive>();
        effect.Initialize(effectAmount);
        return effect;
    }

    private IPassiveEffect CreateCostReductionEffect()
    {
        var effect = ScriptableObject.CreateInstance<ReduceCostPassive>();
        effect.Initialize(effectAmount);
        return effect;
    }
}