using UnityEngine;

public class Carte : MonoBehaviour
{
    public CardDefinition DéfinitionCarte;
    public GameObject Cadre;
    public GameObject Image;

    private SpriteRenderer cadreRenderer;
    private SpriteRenderer imageRenderer;

    void Start()
    {
        if (Cadre != null) cadreRenderer = Cadre.GetComponent<SpriteRenderer>();
        if (Image != null) imageRenderer = Image.GetComponent<SpriteRenderer>();

        if (DéfinitionCarte != null)
        {
            if (cadreRenderer != null)
            {
                cadreRenderer.sprite = DéfinitionCarte.CardImage2;
            }
            else
            {
                Debug.LogWarning("SpriteRenderer du cadre non trouvé !");
            }

            if (imageRenderer != null)
            {
                imageRenderer.sprite = DéfinitionCarte.CardImage;
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
