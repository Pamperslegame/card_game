using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RulesMenuManager : MonoBehaviour
{
    public Button backButton;      // Le bouton "Retour"
    public TMP_Text rulesText;     // Le TextMeshPro pour afficher le contenu
    public string fileName = "RulesContent.txt";

    void Start()
    {
        // Charger le contenu du fichier .txt depuis le dossier Resources
        TextAsset rulesFile = Resources.Load<TextAsset>(fileName);

        if (rulesFile != null)
        {
            // Afficher le contenu du fichier dans le TextMeshPro
            rulesText.text = rulesFile.text;
        }
        else
        {
            Debug.LogError("Fichier de règles introuvable !");
        }

    }
}
