using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    #region Variable    
    [SerializeField] private GameObject cards, Player1, Player2, Player3, Player4;

    [SerializeField] private TMP_Text menu_joueur, menu_round, menu_phase,menu_event;

    [SerializeField] private GameObject DeckPlayer1, DeckPlayer2, DeckPlayer3, DeckPlayer4;

    [SerializeField] private GameObject backMenuButton;


    private TMP_Text Player1Name, Player2Name, Player3Name, Player4Name;

    public int currentPlayer = 1, currentPhase = 1,currentRound = 1;

    private int GoldJoueur1 = 50, GoldJoueur2 = 50, GoldJoueur3= 50, GoldJoueur4 = 50;
    
    private int joueur1Card = 3, joueur2Card = 3, joueur3Card = 3, joueur4Card = 3;

    private TMP_Text Player1Gold, Player2Gold, Player3Gold, Player4Gold;

    public bool[] PlayerInGame = { true, true, true, true };

    public bool FinishGame = false;

    public AudioClip[] attackSounds;
    #endregion

    private int startingPlayerForPhase;


    private int GetFirstActivePlayer()
    {
        for (int i = 0; i < PlayerInGame.Length; i++)
        {
            if (PlayerInGame[i])
            {
                return i + 1; // Les joueurs sont indexés à partir de 1
            }
        }
        return -1; // Gérer le cas où tous les joueurs sont éliminés
    }

    // Jouer un son d'attaque aléatoire
    private AudioSource audioSource;
    public void PlayRandomAttackSound()
    {
        if (attackSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, attackSounds.Length);
            audioSource.volume = 0.2f;
            audioSource.PlayOneShot(attackSounds[randomIndex]);
        }
    }
    
    public void BackMenu()
    {
        Player3.SetActive(false);
        Player4.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
    public void AffichageGagnant()
    {
        FinishGame = true;
        Destroy(menu_round);
        Destroy(menu_phase);
        Destroy(menu_joueur);
        Destroy(DeckPlayer1);
        Destroy(DeckPlayer2);
        Destroy(DeckPlayer3);
        Destroy(DeckPlayer4);
        Destroy(GameObject.Find("Pioche"));
        Destroy(GameObject.Find("UI/Menu/EndRound"));
        Destroy(GameObject.Find("UI/Menu/FF"));

        backMenuButton.SetActive(true);

        if (backMenuButton != null)
        {
            backMenuButton.SetActive(true);
        }
        else
        {
            Debug.Log("BackMenu button not found!");
        }

        menu_event.text = "Partie terminée !";
        Player1Gold.text = "";
        Player2Gold.text = "";
        Player3Gold.text = "";
        Player4Gold.text = "";

        if (PlayerInGame[0] == true) Player1Name.text = "Winner !"; else Player1Name.text = "Looser skill issue!";
        if (PlayerInGame[1] == true) Player2Name.text = "Winner !"; else Player2Name.text = "Looser skill issue!";
        if (PlayerInGame[2] == true && GameManager.joueurs > 2) Player3Name.text = "Winner !"; else Player3Name.text = "Looser skill issue!";
        if (PlayerInGame[3] == true && GameManager.joueurs > 3) Player4Name.text = "Winner !"; else Player4Name.text = "Looser skill issue!";
        
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayerInit();
        RoundInit();
        backMenuButton.SetActive(false);
        startingPlayerForPhase = GetFirstActivePlayer();
        currentPlayer = startingPlayerForPhase;
    }

    private void Update()
    {

        if (!FinishGame)
        {
            CheckGagnantEtPerdant();
            MajDisplayPlayers();
            MajGold();
        }

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



    public bool Check1True()
    {
        if (PlayerInGame[0] && !PlayerInGame[1] && !PlayerInGame[2] && !PlayerInGame[3]) return true;
        else if (!PlayerInGame[0] && PlayerInGame[1] && !PlayerInGame[2] && !PlayerInGame[3]) return true; 
        else if (!PlayerInGame[0] && !PlayerInGame[1] && PlayerInGame[2] && !PlayerInGame[3]) return true;
        else if (!PlayerInGame[0] && !PlayerInGame[1] && !PlayerInGame[2] && PlayerInGame[3]) return true;
        else return false;
    }



    public void CheckGagnantEtPerdant()
    {
        if (Check1True())
        {
            AffichageGagnant();
            return;
        }

        int goldMax = 500;
        bool goldWinnerFound = false;

        if (GoldJoueur1 > goldMax)
        {
            PlayerInGame = new bool[] { true, false, false, false };
            goldWinnerFound = true;
        }
        if (GoldJoueur2 > goldMax)
        {
            PlayerInGame = new bool[] { false, true, false, false };
            goldWinnerFound = true;
        }
        if (GoldJoueur3 > goldMax)
        {
            PlayerInGame = new bool[] { false, false, true, false };
            goldWinnerFound = true;
        }
        if (GoldJoueur4 > goldMax)
        {
            PlayerInGame = new bool[] { false, false, false, true };
            goldWinnerFound = true;
        }

        if (goldWinnerFound)
        {
            AffichageGagnant();
            return;
        }

        int countActifs = 0;
        int indexGagnant = -1;

        for (int i = 0; i < PlayerInGame.Length; i++)
        {
            if (PlayerInGame[i])
            {
                countActifs++;
                indexGagnant = i;
            }
        }

        if (countActifs == 1)
        {
            PlayerInGame = new bool[4];
            PlayerInGame[indexGagnant] = true;

            AffichageGagnant();
        }
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
        PlayerInGame = new bool[4];
        for (int i = 0; i < 4; i++)
        {
            PlayerInGame[i] = (i < GameManager.joueurs);
        }

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



    public void EndRound()
    {
        string[] joueurName = { Player1Name.text, Player2Name.text, Player3Name.text, Player4Name.text };

        do
        {
            currentPlayer = (currentPlayer % GameManager.joueurs) + 1;
        } while (!PlayerInGame[currentPlayer - 1]);

        menu_joueur.text = joueurName[currentPlayer - 1].ToString();

        if (!PlayerInGame[startingPlayerForPhase - 1])
        {
            startingPlayerForPhase = GetFirstActivePlayer();
            if (startingPlayerForPhase == -1) return; 
        }

        if (currentPlayer == startingPlayerForPhase)
        {
            if (currentPhase == 1)
            {
                currentPhase = 2;
                menu_event.text = "";
                menu_phase.text = "Phase : Attack";
                startingPlayerForPhase = GetFirstActivePlayer();
                currentPlayer = startingPlayerForPhase;
            }
            else if (currentPhase == 2)
            {
                currentPhase = 1;
                currentRound++;
                menu_event.text = "";
                menu_phase.text = "Phase : Preparation";
                menu_round.text = "Round :" + currentRound.ToString();
                startingPlayerForPhase = GetFirstActivePlayer();
                currentPlayer = startingPlayerForPhase;

                if (Random.Range(0, 100) > 50)
                {
                    DisplayEvent("+10 golds pour chaques cartes sur votre main!");
                    AddGold(DeckPlayer1.transform.childCount * 10, 1);
                    AddGold(DeckPlayer2.transform.childCount * 10, 2);
                    AddGold(DeckPlayer3.transform.childCount * 10, 3);
                    AddGold(DeckPlayer4.transform.childCount * 10, 4);
                }
            }
        }

        CheckGagnantEtPerdant();
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
