using UnityEngine;
public abstract class PassiveEffect
{
    public string Title { get; protected set; }
    public string Description { get; protected set; }

    // Constructeur
    public PassiveEffect(string title, string description)
    {
        Title = title;
        Description = description;
    }

    // Méthode que chaque passif pourrait implémenter selon sa logique spécifique
    public abstract void Activate(Card card);
}

// Passif Revive (Réanimation)
public class ReviveEffect : PassiveEffect
{
    public ReviveEffect() : base("Réanimation", "Permet à la carte de revenir à la vie avec 1 PV après sa mort.")
    {
    }

    public override void Activate(Card card)
    {
        card.ActivateRevive(true);
        Debug.Log($"{card.name} a activé l'effet de réanimation !");
    }
}

// Passif Block (Blocage)
public class BlockEffect : PassiveEffect
{
    public BlockEffect() : base("Blocage", "Permet de bloquer une attaque et d'éviter les dégâts.")
    {
    }

    public override void Activate(Card card)
    {
        card.ActivateBlock(true);
        Debug.Log($"{card.name} a activé l'effet de blocage !");
    }
}

// Passif Double Strike (Double Attaque)
public class DoubleStrikeEffect : PassiveEffect
{
    public DoubleStrikeEffect() : base("DoubleShot", "Permet à la carte d'attaquer deux fois par tour.")
    {
    }

    public override void Activate(Card card)
    {
        card.ActivateDoubleStrike(true);
        Debug.Log($"{card.name} a activé l'effet de double attaque !");
    }
}
