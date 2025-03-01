using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;  // Le prefab de la carte
    [SerializeField] private Transform spawnPoint;   // Le point d'apparition de la carte
    [SerializeField] private CardDefinition cardDefinition;  // La définition de la carte à instancier

    void Start()
    {
        SpawnCard();
    }

    // Méthode pour instancier une carte
    public void SpawnCard()
    {
        if (cardPrefab == null || spawnPoint == null || cardDefinition == null)
        {
            Debug.LogError("Assurez-vous que CardPrefab, SpawnPoint et CardDefinition sont bien assignés dans l'Inspector.");
            return;
        }

        // Instancier le prefab
        GameObject cardObject = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity);

        // Récupérer le script Card attaché au prefab instancié
        Card cardScript = cardObject.GetComponent<Card>();

        if (cardScript != null)
        {
            // Assigner la définition de la carte à l'objet instancié
            cardScript.CardDefinition = cardDefinition;

            // Initialiser la carte (mettre à jour les valeurs de texte, cadre, image, etc.)
            cardScript.InitializeCard();
        }
        else
        {
            Debug.LogError("Le prefab de carte doit avoir un script 'Card' attaché.");
        }
    }
}
