using UnityEngine;

public class Card : MonoBehaviour
{
    public CardDefinition CardDefinition;
    public GameObject Cadre;
    public GameObject Image;

    private SpriteRenderer cadreRenderer;
    private SpriteRenderer imageRenderer;

    void Start()
    {
        if (Cadre != null) cadreRenderer = Cadre.GetComponent<SpriteRenderer>();
        if (Image != null) imageRenderer = Image.GetComponent<SpriteRenderer>();

        InitializeCard();
    }

    private void InitializeCard()
    {
        if (CardDefinition != null)
        {
            if (cadreRenderer != null)
            {
                cadreRenderer.sprite = CardDefinition.CadreImage;
            }
            else
            {
                Debug.LogWarning("SpriteRenderer du cadre non trouvé !");
            }

            if (imageRenderer != null)
            {
                imageRenderer.sprite = CardDefinition.CardImage;
            }
            else
            {
                Debug.LogWarning("SpriteRenderer de l'image non trouvé !");
            }
        }
        else
        {
            Debug.LogWarning("Aucune définition de carte assignée !");
        }
    }
}
