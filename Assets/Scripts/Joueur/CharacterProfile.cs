using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterProfile", menuName = "Player System/Character Profile")]
public class CharacterProfile : ScriptableObject
{
    [SerializeField] private string characterName;
    [SerializeField] private int baseHp;
    [SerializeField] private int baseXp;
    [SerializeField] private int baseGolds;
    [SerializeField] private int baseDamage;
    [SerializeField] private Sprite avatar;

    public string CharacterName => characterName;
    public int BaseHp => baseHp;
    public int BaseXp => baseXp;
    public int BaseGolds => baseGolds;
    public int BaseDamage => baseDamage;
    public Sprite Avatar => avatar;

    public bool IsValid()
    {
        return baseHp > 0 && baseXp >= 0 && baseGolds >= 0 && baseDamage > 0;
    }

    public string GetCharacterStats()
    {
        return $"Name: {characterName}, HP: {baseHp}, XP: {baseXp}, Golds: {baseGolds}, Damage: {baseDamage}";
    }
}
