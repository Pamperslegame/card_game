using UnityEngine;

[CreateAssetMenu(fileName = "NewPassive", menuName = "Passives/Passive")]
public class Passive : ScriptableObject {
    public string passiveName;
    [TextArea] public string description;
    public Sprite icon;
    public IPassiveEffect effect;

    public void ApplyEffect(Player player) {
        effect?.Apply(player);  
    }
}

