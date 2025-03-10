using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject menu,menuRules,menuStart;
    [SerializeField] private GameObject Joueur3,Joueur4;
    [SerializeField] private TMP_Text nombresJoueurs;
    [SerializeField] private TMP_InputField Player1Name, Player2Name, Player3Name, Player4Name;

    public int joueurs = 2;


    #endregion

    #region Rules
    public void BP_Rules()
    {
        menu.SetActive(false);
        menuRules.SetActive(true);
    }

    public void Rules_return()
    {
        menu.SetActive(true);
        menuRules.SetActive(false);
    }
    #endregion

    #region Start
    public void BP_Start()
    {
        menu.SetActive(false);
        menuStart.SetActive(true);
    }

    public void Start_return()
    {
        menu.SetActive(true);
        menuStart.SetActive(false);
    }

    public void Bp_ChooseNumberPlayers(string symbole)
    {
        if (joueurs < 4 && symbole == "+") joueurs++;
        if (joueurs > 2 && symbole == "-") joueurs--;
        nombresJoueurs.text = joueurs.ToString();
    }




    public void Update()
    {
        if (joueurs == 2) {
            Joueur3.SetActive(false);
            Joueur4.SetActive(false);
        }
        else if (joueurs == 3)
        {
            Joueur3.SetActive(true);
            Joueur4.SetActive(false);
        }
        else if (joueurs == 4)
        {
            Joueur3.SetActive(true);
            Joueur4.SetActive(true);
        }
    }

    public void BP_Launch()
    {
        GameManager.joueurs = joueurs;

        GameManager.joueur1 = Player1Name.text;
        GameManager.joueur2 = Player2Name.text;
        GameManager.joueur3 = Player3Name.text;
        GameManager.joueur4 = Player4Name.text;

        SceneManager.LoadScene("GameScene");
    }
    #endregion

}
