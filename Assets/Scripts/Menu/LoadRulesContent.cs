using System.IO;
using UnityEngine;
using TMPro;

public class LoadRulesContent : MonoBehaviour
{
    public string filePath = "Assets/OtherData/RulesContent.txt"; // Chemin du fichier
    public TMP_Text Rules; // Le composant TMP_Text où afficher le contenu

    void Start()
    {
        LoadTextFromFile();
    }

    void LoadTextFromFile()
    {
        // Lire le fichier texte si il existe
        if (File.Exists(filePath))
        {
            string content = File.ReadAllText(filePath);
            Rules.text = content; // Affecter le texte au composant TMP_Text
        }
        else
        {
            Debug.LogError("Fichier introuvable à l'emplacement: " + filePath);
        }
    }
}
