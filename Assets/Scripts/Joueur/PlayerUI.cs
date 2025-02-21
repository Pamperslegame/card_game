using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;    
    [SerializeField] private TextMeshProUGUI goldText;  
    [SerializeField] private TextMeshProUGUI lvlText;   

    [SerializeField] private Player player;  

    void Start()
    {
        if (player != null)
        {
            UpdatePlayerStats();
        }
        else
        {
            Debug.LogError("Player non assigné dans PlayerUI !");
        }
    }

    public void UpdatePlayerStats()
    {
        if (player != null)
        {
            hpText.text = $"{player.Health}/{player.MaxHealth}";
            goldText.text = $"{player.Golds}";
            lvlText.text = $"XP: {player.Xp}";  
        }
        else
        {
            Debug.LogError("Player non assigné !");
        }
    }
}
