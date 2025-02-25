using UnityEngine;

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
}
