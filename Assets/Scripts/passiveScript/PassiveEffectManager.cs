using UnityEngine;
using System.Collections.Generic; // Pour Dictionary<,>
using System.Linq; // Pour Select, Where, Enumerable, etc.

public class PassiveEffectManager : MonoBehaviour
{
    [SerializeField] private CardDefinition card;

    public void ApplyPassiveEffect(IPassiveEffect passiveEffect)
    {
        if (passiveEffect != null)
        {
            passiveEffect.Apply(card);
        }
    }

    public void ApplyRandomPassiveEffect(ScriptableObject[] passives)
    {
        if (passives.Length == 0)
            return;

        int randomIndex = Random.Range(0, passives.Length);
        IPassiveEffect selectedPassive = passives[randomIndex] as IPassiveEffect;
        ApplyPassiveEffect(selectedPassive);
    }

    public PassiveRarity GetRandomRarity()
    {
        Dictionary<PassiveRarity, float> rarityProbabilities = new()
        {
            { PassiveRarity.Common, 0.6f },
            { PassiveRarity.Rare, 0.3f },
            { PassiveRarity.Epic, 0.08f },
            { PassiveRarity.Legendary, 0.02f }
        };

        float randomValue = Random.value;
        float cumulativeProbability = 0f;

        foreach (var kvp in rarityProbabilities)
        {
            cumulativeProbability += kvp.Value;
            if (randomValue <= cumulativeProbability)
            {
                return kvp.Key;
            }
        }

        return PassiveRarity.Common;
    }

    public IPassiveEffect[] GetThreePassivesOfRarity(ScriptableObject[] passives, PassiveRarity rarity)
    {
        var filteredPassives = passives
            .Select(p => p as IPassiveEffect)
            .Where(p => p != null && p.Rarity == rarity)
            .ToArray();

        if (filteredPassives.Length <= 3)
        {
            return filteredPassives;
        }

        var selectedPassives = new IPassiveEffect[3];
        var availableIndices = Enumerable.Range(0, filteredPassives.Length).ToList();

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            selectedPassives[i] = filteredPassives[availableIndices[randomIndex]];
            availableIndices.RemoveAt(randomIndex);
        }

        return selectedPassives;
    }
}