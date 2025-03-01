using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerNameManager : MonoBehaviour
{
    public GameObject inputFieldPrefab; // Préfab de l'InputField
    public Transform playerNameContainer; // Conteneur des InputFields

    void Start()
    {
        int playerCount = PlayerPrefs.GetInt("PlayerCount", 2); // Récupérer le nombre de joueurs depuis PlayerPrefs

        // Instancier les InputFields pour chaque joueur
        for (int i = 0; i < playerCount; i++)
        {
            // Créer un nouveau champ de texte pour chaque joueur
            GameObject newInputField = Instantiate(inputFieldPrefab, playerNameContainer);

            // Accéder au composant TMP_InputField pour changer son placeholder
            TMP_InputField inputField = newInputField.GetComponent<TMP_InputField>();
            inputField.placeholder.GetComponent<TextMeshProUGUI>().text = $"Joueur {i + 1}"; // Définir le placeholder pour chaque joueur
        }
    }

    public void OnStartButtonClicked()
    {
        int playerCount = PlayerPrefs.GetInt("PlayerCount", 2);

        List<string> playerNames = new List<string>();
        bool allNamesFilled = true;

        // Vérifier si tous les champs sont remplis
        for (int i = 0; i < playerCount; i++)
        {
            TMP_InputField inputField = playerNameContainer.GetChild(i).GetComponent<TMP_InputField>();
            string playerName = inputField.text;

            if (string.IsNullOrEmpty(playerName))  // Si un champ est vide
            {
                allNamesFilled = false;
                Debug.LogError($"Le pseudo du Joueur {i + 1} n'est pas rempli !");
            }
            else
            {
                playerNames.Add(playerName);
            }
        }

        // Si tous les pseudos sont remplis
        if (allNamesFilled)
        {
            // Sauvegarder les pseudos, les points de vie, les golds et l'XP dans PlayerPrefs
            for (int i = 0; i < playerNames.Count; i++)
            {
                string playerName = playerNames[i];
                PlayerPrefs.SetString($"Player{i + 1}Name", playerName);

                // Points de vie par défaut
                PlayerPrefs.SetInt($"Player{i + 1}_Health", 100);

                // Gold et XP par défaut
                PlayerPrefs.SetInt($"Player{i + 1}_Gold", 20);
                PlayerPrefs.SetInt($"Player{i + 1}_XP", 20);
            }

            Debug.Log("Pseudos des joueurs : ");
            for (int i = 0; i < playerNames.Count; i++)
            {
                string playerName = playerNames[i];
                int playerHealth = PlayerPrefs.GetInt($"Player{i + 1}_Health", 100); // Récupérer les HP (100 par défaut)
                int playerGold = PlayerPrefs.GetInt($"Player{i + 1}_Gold", 20); // Récupérer les Gold (20 par défaut)
                // int playerXP = PlayerPrefs.GetInt($"Player{i + 1}_XP", 20); // Récupérer l'XP (20 par défaut)

                Debug.Log($"{playerName} - HP = {playerHealth}, Gold = {playerGold}"); // add : XP = {playerXP} pr lire
            }

            MatchupManager matchupManager = new MatchupManager();
            List<string> matchups = matchupManager.GenerateMatchups(playerCount);

            // Afficher les matchups dans la console (tu peux aussi les utiliser dans le jeu)
            foreach (var matchup in matchups)
            {
                Debug.Log(matchup);
            }
            SceneManager.LoadScene("Plateau");
            }
            else
            {
                Debug.LogError("Veuillez remplir tous les champs de pseudo !");
            }
    }
}
