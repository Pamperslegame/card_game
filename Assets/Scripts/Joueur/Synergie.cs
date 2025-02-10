using UnityEngine;

[System.Serializable]
public class Synergie
{
    [SerializeField] private SynergieType type;
    public SynergieType Type => type;

    public Synergie(SynergieType type)
    {
        this.type = type;
    }

    public void AppliquerEffetSynergie(CardDefinition card, int nombreCartesEnJeu, PlayerGame playerGame)
    {
        if (nombreCartesEnJeu < 2) return;

        switch (Type)
        {
            case SynergieType.Robot:
                PiocheSupplementaire(playerGame, nombreCartesEnJeu);
                break;
            case SynergieType.Araignee:
                ReductionCout(card, nombreCartesEnJeu);
                break;
            case SynergieType.Chevalier:
                BuffDamage(card, nombreCartesEnJeu);
                break;
            case SynergieType.ChevalierDeLaNuit:
                AppliquerPoison(card, nombreCartesEnJeu);
                break;
        }
    }

    private void PiocheSupplementaire(PlayerGame player, int nombreCartes)
    {
        if (player.handManager != null)
        {
            player.handManager.handSize = nombreCartes == 2 ? 5 : (nombreCartes == 4 ? 6 : player.handManager.handSize);
        }
    }

    private void BuffDamage(CardDefinition card, int nombreCartes)
    {
        card.ApplyDamageBonus(nombreCartes == 2 ? 1 : (nombreCartes == 4 ? 3 : 0));
    }

    private void AppliquerPoison(CardDefinition card, int nombreCartes)
    {
        Debug.Log($"Effet Chevalier de la Nuit : Poison appliqué avec une intensité de {nombreCartes - 1}.");
    }

    private void ReductionCout(CardDefinition card, int nombreCartes)
    {
        card.ApplyHealthBonus(nombreCartes == 2 ? 1 : (nombreCartes == 4 ? 2 : 0)); 
    }
}
