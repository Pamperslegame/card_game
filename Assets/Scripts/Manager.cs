using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{

    #region Variable    
    [SerializeField] private GameObject cards, Player1, Player2, Player3, Player4;

    [SerializeField] private TMP_Text menu_joueur, menu_round, menu_phase;

    [SerializeField] private GameObject DeckPlayer1, DeckPlayer2, DeckPlayer3, DeckPlayer4;

    private TMP_Text Player1Name, Player2Name, Player3Name, Player4Name;

    public int currentPlayer = 1, currentPhase = 1,currentRound = 1;

    private int GoldJoueur1 = 30, GoldJoueur2 = 30, GoldJoueur3= 30, GoldJoueur4 = 30;
    
    private int joueur1Card = 3, joueur2Card = 3, joueur3Card = 3, joueur4Card = 3;

    private TMP_Text Player1Gold, Player2Gold, Player3Gold, Player4Gold;
    #endregion

    void Start()
    {
        PlayerInit();
        RoundInit();
    }

    // Décrémentation du nbre de carte après en avoir perdu une
    public void DecrementationCountCard(int currentPlayer)
    {
        Debug.Log($"{joueur1Card}{joueur2Card}{joueur3Card}{joueur4Card}");
        switch (currentPlayer)
        {

            case 1: joueur1Card--; break;
            case 2: joueur2Card--; break;
            case 3: joueur3Card--; break;
            case 4: joueur4Card--; break;
        }
        Debug.Log($"{joueur1Card}{joueur2Card}{joueur3Card}{joueur4Card}");
    }

    // maj gold sur plateau
    public void MajGold()
    {
        Player1Gold.text = "Gold :" + GoldJoueur1.ToString();
        Player2Gold.text = "Gold :" + GoldJoueur2.ToString();
        Player3Gold.text = "Gold :" + GoldJoueur3.ToString();
        Player4Gold.text = "Gold :" + GoldJoueur4.ToString();
    }

    // add gold a un joueur
    public void AddGold(int GoldLost,int currentPlayer)
    {
        switch (currentPlayer)
        {

            case 1: GoldJoueur1+= GoldLost; break;
            case 2: GoldJoueur2 += GoldLost; break;
            case 3: GoldJoueur3 += GoldLost; break;
            case 4: GoldJoueur4 += GoldLost; break;
        }

        MajGold();

    }

    public void Awake()
    {

        // Récupérer pseudos :
        Player1Name = Player1.transform.Find("Player1Pseudo").GetComponent<TMP_Text>();
        Player2Name = Player2.transform.Find("Player2Pseudo").GetComponent<TMP_Text>();
        Player3Name = Player3.transform.Find("Player3Pseudo").GetComponent<TMP_Text>();
        Player4Name = Player4.transform.Find("Player4Pseudo").GetComponent<TMP_Text>();

        // Récupérer golds :
        Player1Gold = Player1.transform.Find("GoldPlayer1").GetComponent<TMP_Text>();
        Player2Gold = Player2.transform.Find("GoldPlayer2").GetComponent<TMP_Text>();
        Player3Gold = Player3.transform.Find("GoldPlayer3").GetComponent<TMP_Text>();
        Player4Gold = Player4.transform.Find("GoldPlayer4").GetComponent<TMP_Text>();

    }

    // initialiser les canva des joueurs
    public void PlayerInit()
    {
        if (GameManager.joueurs == 3)
        {
            Player3.SetActive(true);
            DeckPlayer3.SetActive(true);

        }
        if (GameManager.joueurs > 3)
        {
            DeckPlayer3.SetActive(true);
            DeckPlayer4.SetActive(true);
            Player3.SetActive(true);
            Player4.SetActive(true);
        }
    }

    // initilaiser rounds :
    public void RoundInit()
    {
        menu_joueur.text = Player1Name.text;
        menu_round.text = "Round : 1";
        menu_phase.text = "Phase : Preparation";

    }



    // fin d'un round 2 phases attack et préparation
    public void EndRound() {

        string[] joueurName = { Player1Name.text, Player2Name.text, Player3Name.text, Player4Name.text };
        int[] joueurGold = { GoldJoueur1, GoldJoueur2, GoldJoueur3, GoldJoueur4 };

        currentPlayer = (currentPlayer % GameManager.joueurs) + 1;

        menu_joueur.text = joueurName[currentPlayer - 1].ToString();


        if (currentPlayer == 1 && currentPhase == 1)
        {
            currentPhase++;
            menu_phase.text = "Phase : Attack";

        }

        else if (currentPlayer == 1 && currentPhase == 2)
        {
            currentPhase--;
            currentRound += 1;
            menu_phase.text = "Phase : Preparation";
            menu_round.text = "Round :" + currentRound.ToString();

            AddGold(10,1); AddGold(10, 2); AddGold(10, 3); AddGold(10, 4);

            MajGold();
        }
    }



    public void pioche()
    {
        if (currentPhase == 1)
        {
            if (currentPlayer == 1 && joueur1Card != 6 && GoldJoueur1 > 9)
            {
                AddGold(-10, 1);
                joueur1Card += 1;


                Instantiate(cards, DeckPlayer1.transform.position, Quaternion.identity, DeckPlayer1.transform);
            }
            else if (currentPlayer == 2 && joueur2Card != 6 && GoldJoueur2 > 9)
            {
                GoldJoueur2 -= 10;
                joueur2Card += 1;

                Instantiate(cards, DeckPlayer2.transform.position, Quaternion.identity, DeckPlayer2.transform);
            }
            else if (currentPlayer == 3 && joueur3Card != 6 && GoldJoueur3 > 9)
            {
                GoldJoueur3 -= 10;
                joueur3Card += 1;

                Instantiate(cards, DeckPlayer3.transform.position, Quaternion.identity, DeckPlayer3.transform);
            }
            else if (currentPlayer == 4 && joueur4Card != 6 && GoldJoueur4 > 9)
            {
                GoldJoueur4 -= 10;
                joueur4Card += 1;

                Instantiate(cards, DeckPlayer4.transform.position, Quaternion.identity, DeckPlayer4.transform);
            }
        }
        MajGold();
    }

}
