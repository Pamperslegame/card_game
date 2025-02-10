using System.Collections.Generic;
using UnityEngine;

public class PlayerGame : MonoBehaviour
{
    public List<Card> cartesEnJeu = new List<Card>();
    public HandManager handManager; 

    public void MettreAJourSynergies()
    {
        Dictionary<SynergieType, int> synergieCounts = new Dictionary<SynergieType, int>();

        foreach (Card carte in cartesEnJeu)
        {
            if (carte.CardDefinition.Synergie != null)
            {
                SynergieType type = carte.CardDefinition.Synergie.Type;

                if (!synergieCounts.ContainsKey(type))
                    synergieCounts[type] = 0;

                synergieCounts[type]++;
            }
        }

        foreach (Card carte in cartesEnJeu)
        {
            Synergie synergie = carte.CardDefinition.Synergie;

            if (synergie != null && synergieCounts.TryGetValue(synergie.Type, out int count))
            {
                synergie.AppliquerEffetSynergie(carte.CardDefinition, count, this);
            }
        }
    }

    public void AjouterCarte(Card carte)
    {
        cartesEnJeu.Add(carte);
        MettreAJourSynergies();
    }

    public void RetirerCarte(Card carte)
    {
        cartesEnJeu.Remove(carte);
        MettreAJourSynergies();
    }
}
