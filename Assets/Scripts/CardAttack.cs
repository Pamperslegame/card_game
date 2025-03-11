using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardAttack : MonoBehaviour
{
    private static CardAttack select;
    [SerializeField] private GameObject cards;
    [SerializeField] private string joueurTag;
    [SerializeField] private Manager manager;


    private void Awake()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
    }
    public void OnClick()
    {

        if (manager.currentPhase == 2)
        {

                string namePlayer = GetPlayerName(manager.currentPlayer);
            Transform playerDeck = GameObject.FindWithTag(namePlayer).transform;

            bool isMyCard = transform.IsChildOf(playerDeck);

            if (!isMyCard && select == null) return;
            if (select == this) Deselect();

            else
            {
                if (isMyCard)
                {
                    select?.Deselect();
                    Select();
                }
                else
                {
                    Attack(select, this);
                    select.Deselect();
                }
            }
        }
    }

    // pour avoir nom du joueur (qui est aussi le tag)
    public string GetPlayerName(int currentPlayer)
    {
        // Vérifie que le joueur est toujours actif
        if (currentPlayer < 1 || currentPlayer > 4 || !manager.PlayerInGame[currentPlayer - 1])
            return "Invalid";

        return $"Joueur{currentPlayer}";
    }

    // attaquer un joueur
    void Attack(CardAttack Attaquant, CardAttack Enemie)
    {
        int rdmRevive = Random.Range(0, 100);
        int rdmAbsorbe = Random.Range(0, 100);
        int rdmRetentless = Random.Range(0, 100);

        int luckRevive = 30;
        int luckAbsorbe = 20;
        int luckRetentless = 10;



        // stats des joueurs
        TMPro.TextMeshProUGUI[] enemiePlayerStats = Enemie.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        TMPro.TextMeshProUGUI[] attaquantPlayerStats = Attaquant.GetComponentsInChildren<TMPro.TextMeshProUGUI>();


        // je fais la soustraction x2 si y'a retentless
        int enemyHP = int.Parse(enemiePlayerStats[1].text);
        int attackerDamage = int.Parse(attaquantPlayerStats[0].text);

        // gestion moche des passifs :
        if (rdmRetentless < luckRetentless)
        {
            enemyHP -= attackerDamage * 2;
            manager.DisplayEvent("Retentless !");
        }
        else if (rdmAbsorbe < luckAbsorbe)
        {
            manager.DisplayEvent("Absorbe !");
        }

        else
        {
            enemyHP -= attackerDamage;
        }

        // set le nouveau hp sur l'attaqu�
        enemiePlayerStats[1].text = enemyHP.ToString();

        // si hp <= 0 je supprime la carte et je d�cr�mente le countcard de l'�nemie ainsi que les modifs de gold
        if (enemyHP <= 0)
        {

            manager.PlayRandomAttackSound();

            if (rdmRevive < luckRevive)
            {
                enemiePlayerStats[1].text = "30";
                manager.DisplayEvent("Revive !");
            }
            else
            {

                int enemyPlayerNumber = GetPlayerNumber(Enemie.transform.parent.name);
                Destroy(Enemie.gameObject);

                // retirer gold au joueur attaqu�
                manager.AddGold(-5, enemyPlayerNumber);

                // ajouter gold au joueur qui a attaaqu�
                manager.AddGold(5, manager.currentPlayer);

                // d�cr�menter soncompteur
                manager.DecrementationCountCard(enemyPlayerNumber);
            }
        }
        manager.EndRound();
    }

    int GetPlayerNumber(string playerName)
    {
        switch (playerName)
        {
            case "DeckJoueur1": return 1;
            case "DeckJoueur2": return 2;
            case "DeckJoueur3": return 3;
            case "DeckJoueur4": return 4;
            default: return 0; // Ou gérer autrement si c'est un cas invalide
        }
    }

    private void Update()
    {
        if (select == null) cards.transform.localScale = new Vector3(0.20f, 0.20f, 0.20f);

    }

    void Deselect()
    {
        cards.transform.localScale = new Vector3(0.20f, 0.20f, 0.20f);
        select = null;
    }
    void Select()
    {
        cards.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        select = this;
    }
}
