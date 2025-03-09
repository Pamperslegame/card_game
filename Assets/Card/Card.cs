using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardStats carteActive;
    [SerializeField] private SpriteRenderer Frame, Monster;
    [SerializeField] private TMP_Text Pv, dmg;
    public CardStats[] card;

    void Start()
    {
        int randomIndex = Random.Range(0, card.Length);
        carteActive = card[randomIndex];

        Pv.text = carteActive.pv.ToString();
        dmg.text = carteActive.damage.ToString();
        Frame.sprite = carteActive.rarity2;
        Monster.sprite = carteActive.monster;
    }
}
