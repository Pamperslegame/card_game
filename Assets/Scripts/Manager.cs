using System.ComponentModel;
using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{

    #region Variable    
    [SerializeField] private GameObject cards, Player1, Player2, Player3, Player4;

    [SerializeField] private TMP_Text menu_joueur, menu_round, menu_phase,menu_event;

    [SerializeField] private GameObject DeckPlayer1, DeckPlayer2, DeckPlayer3, DeckPlayer4;

    private TMP_Text Player1Name, Player2Name, Player3Name, Player4Name;

    public int currentPlayer = 1, currentPhase = 1,currentRound = 1;

    private int GoldJoueur1 = 50, GoldJoueur2 = 50, GoldJoueur3= 50, GoldJoueur4 = 50;
    
    private int joueur1Card = 3, joueur2Card = 3, joueur3Card = 3, joueur4Card = 3;

    private TMP_Text Player1Gold, Player2Gold, Player3Gold, Player4Gold;

    public bool[] PlayerInGame = { true, true, true, false };

    #endregion

    // le jeu est terminé et j'affiche le gagnant
    public void AffichageGagnant(int gagnant)
    {
        Debug.Log(gagnant);
    }

    void Start()
    {
        PlayerInit();
        RoundInit();
    }

    private void Update()
    {

        MajDisplayPlayers();
        MajGold();

        CheckGagnantEtPerdant();
    }

    // bouton pour forfate un joueur
    public void BP_Forfate()
    {
        PlayerInGame[currentPlayer-1] = false;
        EndRound();
    }

    // Décrémentation du nbre de carte après en avoir perdu une
    public void DecrementationCountCard(int currentPlayer)
    {
        switch (currentPlayer)
        {

            case 1: joueur1Card--; break;
            case 2: joueur2Card--; break;
            case 3: joueur3Card--; break;
            case 4: joueur4Card--; break;
        }
    }

    // maj gold all joueur sur plateau
    public void MajGold()
    {

        if (PlayerInGame[0]) Player1Gold.text = "Gold :" + GoldJoueur1.ToString();
        if (PlayerInGame[1]) Player2Gold.text = "Gold :" + GoldJoueur2.ToString();
        if (PlayerInGame[2]) Player3Gold.text = "Gold :" + GoldJoueur3.ToString();
        if (PlayerInGame[3]) Player4Gold.text = "Gold :" + GoldJoueur4.ToString();
    }

    // add gold a un joueur spécifique
    public void AddGold(int GoldLost,int currentPlayer)
    {
        switch (currentPlayer)
        {
            case 1: GoldJoueur1+= GoldLost; break;
            case 2: GoldJoueur2 += GoldLost; break;
            case 3: GoldJoueur3 += GoldLost; break;
            case 4: GoldJoueur4 += GoldLost; break;
        }

    }



    // si y'a qu'un seul joueur ça va return un indice
    public int GetLastPlayerIndex()
    {
        int lastIndex = 0;
        int count = 0;

        for (int i = 0; i < PlayerInGame.Length; i++)
        {
            if (PlayerInGame[i])
            {
                count++;
                lastIndex = i + 1;
            }
        }

        return (count == 1) ? lastIndex : 0;
    }

    // check si ya gagnant et afficher qui a gagné avec bouton retour
    public void CheckGagnantEtPerdant()
    {
        // si y'a plus qu'un seul gars
        int gagnant = GetLastPlayerIndex();
        if (gagnant != 0) AffichageGagnant(GetLastPlayerIndex());

        // Si un gars a dépassé 1k gold gg a lui
        if (GoldJoueur1 > 1000) AffichageGagnant(1);
        if (GoldJoueur2 > 1000) AffichageGagnant(2);
        if (GoldJoueur3 > 1000) AffichageGagnant(3);
        if (GoldJoueur4 > 1000) AffichageGagnant(4);
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


    // mettre a jour l'UI si forfate ou alors 
    public void MajDisplayPlayers()
    {
        // si y'a 0 cartes le joueurs est perdu
        if (DeckPlayer1.transform.childCount == 0) PlayerInGame[0] = false;
        if (DeckPlayer2.transform.childCount == 0) PlayerInGame[1] = false;
        if (DeckPlayer3.transform.childCount == 0) PlayerInGame[2] = false;
        if (DeckPlayer4.transform.childCount == 0) PlayerInGame[3] = false;

        // Si le joueur est perdu alors on supprime son UI
        if (!PlayerInGame[0])
            {
                DeckPlayer1.SetActive(false);
                Player1Gold.text = "Perdu !";

            }
        if (!PlayerInGame[1])
        {
            DeckPlayer2.SetActive(false);
            Player2Gold.text = "Perdu !";

        }
        if (!PlayerInGame[2])
        {
            DeckPlayer3.SetActive(false);
            Player3Gold.text = "Perdu !";

        }
        if (!PlayerInGame[3])
        {
            DeckPlayer4.SetActive(false);
            Player4Gold.text = "Perdu !";

        }
    }


    // initilaiser rounds :
    public void RoundInit()
    {
        menu_joueur.text = Player1Name.text;
        menu_round.text = "Round : 1";
        menu_phase.text = "Phase : Preparation";

    }


    // afficher les events
    public void DisplayEvent(string Event)
    {
        menu_event.text = Event.ToString();
    }


    // fin d'un round 2 phases attack et préparation
    public void EndRound() {

        string[] joueurName = { Player1Name.text, Player2Name.text, Player3Name.text, Player4Name.text };
        int[] joueurGold = { GoldJoueur1, GoldJoueur2, GoldJoueur3, GoldJoueur4 };

        // verif de si le joueur a abandonné ou alors a perdu
        do
        {
            currentPlayer = (currentPlayer % GameManager.joueurs) + 1;
        } while (!PlayerInGame[currentPlayer - 1]);


        menu_joueur.text = joueurName[currentPlayer - 1].ToString();


        if (currentPlayer == 1 && currentPhase == 1)
        {
            currentPhase++;
            menu_event.text = "";
            menu_phase.text = "Phase : Attack";

        }

        else if (currentPlayer == 1 && currentPhase == 2)
        {
            currentPhase--;
            currentRound += 1;
            menu_event.text = "";
            menu_phase.text = "Phase : Preparation";
            menu_round.text = "Round :" + currentRound.ToString();

            // DeckPlayer1.transform.childCount on rajoute le nombre de carte fois le nombre de gold (event) 50 % de chance
            if (Random.Range(0, 100) > 50)
            {
                DisplayEvent("+10 golds pour chaques cartes sur votre main!");
                AddGold(DeckPlayer1.transform.childCount * 10, 1); AddGold(DeckPlayer2.transform.childCount * 10, 2); AddGold(DeckPlayer3.transform.childCount * 10, 3); AddGold(DeckPlayer4.transform.childCount * 10, 4);
            }
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
    }

}
