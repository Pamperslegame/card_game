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


    private int luckPassive = 25; 

    public int GetLuckPassive()
    {
        return luckPassive;
    }

    public void SetLuckPassive(int value)
    {
        luckPassive = value;
    }



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
        // V�rifie que le joueur est toujours actif
        if (currentPlayer < 1 || currentPlayer > 4 || !manager.PlayerInGame[currentPlayer - 1])
            return "Invalid";

        return $"Joueur{currentPlayer}";
    }

    public void Attack(CardAttack attaquant, CardAttack enemie)
    {
        int damage = CalculateDamage(attaquant);
        ApplyDamage(enemie, damage);
    }

    private int CalculateDamage(CardAttack attaquant)
    {
        int attackerDamage = int.Parse(attaquant.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text);

        if (IsRelentlessTriggered())
        {
            manager.DisplayEvent("Relentless !");
            return attackerDamage * 2;
        }

        if (IsAbsorbeTriggered())
        {
            manager.DisplayEvent("Absorbe !");
            return 0;
        }

        return attackerDamage;
    }

    private bool IsRelentlessTriggered()
    {
        return Random.Range(0, 100) < luckPassive;
    }

    private bool IsAbsorbeTriggered()
    {
        return Random.Range(0, 100) < luckPassive;
    }

    private void ApplyDamage(CardAttack enemie, int damage)
    {
        TMPro.TextMeshProUGUI[] enemieStats = enemie.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        int enemyHP = int.Parse(enemieStats[1].text);
        enemyHP -= damage;
        enemieStats[1].text = enemyHP.ToString();

        if (enemyHP <= 0)
        {
            HandleDeath(enemie);
        }
        else
        {
            manager.EndRound();
        }
    }

    private void HandleDeath(CardAttack enemie)
    {
        if (IsReviveTriggered())
        {
            enemie.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1].text = "30";
            manager.DisplayEvent("Revive !");
        }
        else
        {
            int enemyPlayerNumber = GetPlayerNumber(enemie.transform.parent.name);
            Destroy(enemie.gameObject);
            manager.AddGold(-5, enemyPlayerNumber);
            manager.AddGold(5, manager.currentPlayer);
            manager.DecrementationCountCard(enemyPlayerNumber);
            manager.PlayRandomAttackSound();
        }

        manager.EndRound();
    }

    private bool IsReviveTriggered()
    {
        return Random.Range(0, 100) < luckPassive;
    }

    int GetPlayerNumber(string playerName)
    {
        switch (playerName)
        {
            case "DeckJoueur1": return 1;
            case "DeckJoueur2": return 2;
            case "DeckJoueur3": return 3;
            case "DeckJoueur4": return 4;
            default: return 0; // Ou g�rer autrement si c'est un cas invalide
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
