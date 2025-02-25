public interface IPassiveEffect
{
    string Title { get; }
    string Description { get; }
    PassiveRarity Rarity { get; }
    void Apply(CardDefinition card);
}