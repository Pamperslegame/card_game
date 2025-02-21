using UnityEngine;

public class Synergie : ScriptableObject
{
    [SerializeField] private SynergieType synergieType;
    [SerializeField] private int effectAmount;

    public SynergieType SynergieType => synergieType;
    public int EffectAmount => effectAmount;

    
    public void ApplySynergyEffect(CardDefinition card)
    {
        switch (synergieType)
        {
            case SynergieType.Robot:
                ApplyRobotSynergy(card); 
                break;
            case SynergieType.Chevalier:
                ApplyChevalierSynergy(card); 
                break;
            case SynergieType.Nuit:
                ApplyNuitSynergy(card); 
                break;
            case SynergieType.Araignee:
                ApplyAraigneeSynergy(card); 
                break;
        }
    }

    private void ApplyRobotSynergy(CardDefinition card)
    {
    }

    private void ApplyChevalierSynergy(CardDefinition card)
    {
        card.ApplyDamageBonus(effectAmount);
    }

    private void ApplyNuitSynergy(CardDefinition card)
    {
    }

    private void ApplyAraigneeSynergy(CardDefinition card)
    {
        card.ApplyCostReduction(effectAmount);
    }
}