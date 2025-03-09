using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardStats carteActive;
    [SerializeField] private SpriteRenderer Frame, Monster;
    [SerializeField] private TMP_Text Pv, dmg;
    
    [SerializeField] private int ChampLuck = 5;

    public CardStats[] card;
    public CardStats[] cardsChampions;
    static bool picked = false;

    void Start()
    {
        
        int rdmChamp = Random.Range(0, 100);

        if (rdmChamp < ChampLuck && !picked)
        {
            int randomIndex = Random.Range(0, cardsChampions.Length);
            carteActive = cardsChampions[randomIndex];
            picked = true;
        }
        else
        {
            int randomIndex = Random.Range(0, card.Length);
            carteActive = card[randomIndex];
        }

        Pv.text = carteActive.pv.ToString();
        dmg.text = carteActive.damage.ToString();
        Frame.sprite = carteActive.rarity2;
        Monster.sprite = carteActive.monster;
    }
}
