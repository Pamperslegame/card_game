using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterProfile", menuName = "Player System/Character Profile")]
public class CharacterProfile : ScriptableObject
{
    [SerializeField] private string characterName;
    [SerializeField] private int baseHp;
    [SerializeField] private int baseXp;
    [SerializeField] private int baseGolds;
    [SerializeField] private int baseDamage;

    public string CharacterName => characterName;
    public int BaseHp => baseHp;
    public int BaseXp => baseXp;
    public int BaseGolds => baseGolds;
    public int BaseDamage => baseDamage;
}
