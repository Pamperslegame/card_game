using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RulesMenuManager : MonoBehaviour
{
    public Button backButton;
    public TMP_Text rulesText; 
    public string fileName = "RulesContent.txt";

    void Start()
    {
        TextAsset rulesFile = Resources.Load<TextAsset>(fileName);

        if (rulesFile != null)
        {
            rulesText.text = rulesFile.text;
        }
        else
        {
            Debug.LogError("Fichier de règles introuvable !");
        }

    }
}
