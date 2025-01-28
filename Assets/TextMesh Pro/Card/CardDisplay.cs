using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [Header("UI Components")]
    public Image cardFrame;
    public Image cardPicture;
    public Text cardNameText;
    public Text cardHealthText;
    public Text cardDamageText;

    public void Setup(CardDefinition card)
    {
        cardFrame.sprite = card.cadre;
        cardPicture.sprite = card.picture;
        cardNameText.text = card.cardName;
        cardHealthText.text = "Health: " + card.health.ToString();
        cardDamageText.text = "Damage: " + card.damage.ToString();
    }
}

