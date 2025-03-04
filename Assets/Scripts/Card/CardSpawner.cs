using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab; 
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private CardDefinition cardDefinition;

    void Start()
    {
        SpawnCard();
    }

    public void SpawnCard()
    {
        if (cardPrefab == null || spawnPoint == null || cardDefinition == null)
        {
            Debug.LogError("Assurez-vous que CardPrefab, SpawnPoint et CardDefinition sont bien assignés dans l'Inspector.");
            return;
        }

        GameObject cardObject = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity);

        Card cardScript = cardObject.GetComponent<Card>();

        if (cardScript != null)
        {
            cardScript.CardDefinition = cardDefinition;

            cardScript.InitializeCard();
        }
        else
        {
            Debug.LogError("Le prefab de carte doit avoir un script 'Card' attaché.");
        }
    }
}
