using UnityEngine;

public class Synergie : ScriptableObject
{
    [SerializeField] private SynergieType synergieType;
    [SerializeField] private int effectAmount;

    public SynergieType SynergieType => synergieType;
    public int EffectAmount => effectAmount;

}