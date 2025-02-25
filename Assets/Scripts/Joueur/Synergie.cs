using UnityEngine;

public class Synergie : ScriptableObject
{
    [SerializeField] private SynergieType synergieType;
    [SerializeField] private int effectAmount;

    public SynergieType SynergieType => synergieType;
    public int EffectAmount => effectAmount;

    
    public void ApplySynergyEffect(CardDefinition cardDefinition, Card card)
    {
        switch (synergieType)
        {
            case SynergieType.Robot:
                ApplyRobotSynergy(cardDefinition, card); 
                break;
            case SynergieType.Chevalier:
                ApplyChevalierSynergy(cardDefinition, card); 
                break;
            case SynergieType.Nuit:
                ApplyNuitSynergy(cardDefinition, card); 
                break;
            case SynergieType.Araignee:
                ApplyAraigneeSynergy(cardDefinition, card); 
                break;
        }
    }

    private void ApplyRobotSynergy(CardDefinition cardDefinition, Card card)
    {
        Debug.Log("Synergie Robot appliquée !");
    }

    private void ApplyChevalierSynergy(CardDefinition cardDefinition, Card card)
    {
        cardDefinition.ApplyDamageBonus(effectAmount);
        Debug.Log($"Bonus de dégâts de {effectAmount} appliqué grâce à la synergie Chevalier !");
    }

    private void ApplyNuitSynergy(CardDefinition cardDefinition, Card card)
    {
        Debug.Log("Synergie Nuit appliquée !");
    }

    private void ApplyAraigneeSynergy(CardDefinition cardDefinition, Card card)
    {
        cardDefinition.ApplyCostReduction(effectAmount);
        Debug.Log($"Réduction de coût de {effectAmount} appliquée grâce à la synergie Araignée !");
    }
}