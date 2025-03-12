using NUnit.Framework;
using UnityEngine;

public class CardAttackTests
{
    public int luckPassive = 25;
    [Test]
    public void Attack_DealsCorrectDamage()
    {
        GameObject attackerObject = new GameObject();
        CardAttack attacker = attackerObject.AddComponent<CardAttack>();
        GameObject enemyObject = new GameObject();
        CardAttack enemy = enemyObject.AddComponent<CardAttack>();

        var attackerText = new GameObject().AddComponent<TMPro.TextMeshProUGUI>();
        attackerText.text = "10"; // Attaque de 10
        attackerText.transform.SetParent(attackerObject.transform);

        var enemyText = new GameObject().AddComponent<TMPro.TextMeshProUGUI>();
        enemyText.text = "30"; // HP de 30
        enemyText.transform.SetParent(enemyObject.transform);

        attacker.Attack(attacker, enemy);

        TMPro.TextMeshProUGUI[] enemyStats = enemy.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        int enemyHP = int.Parse(enemyStats[1].text);
        Assert.AreEqual(20, enemyHP);
    }

    [Test]
    public void Absorbe_BlocksDamage()
    {
        GameObject attackerObject = new GameObject();
        CardAttack attacker = attackerObject.AddComponent<CardAttack>();
        GameObject enemyObject = new GameObject();
        CardAttack enemy = enemyObject.AddComponent<CardAttack>();

        var attackerText = new GameObject().AddComponent<TMPro.TextMeshProUGUI>();
        attackerText.text = "10"; 
        attackerText.transform.SetParent(attackerObject.transform);

        var enemyText = new GameObject().AddComponent<TMPro.TextMeshProUGUI>();
        enemyText.text = "30"; 
        enemyText.transform.SetParent(enemyObject.transform);

        enemy.SetLuckPassive(100); 

        attacker.Attack(attacker, enemy);

        TMPro.TextMeshProUGUI[] enemyStats = enemy.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        int enemyHP = int.Parse(enemyStats[1].text);
        Assert.AreEqual(30, enemyHP); 
    }

    [Test]
    public void Revive_ResurrectsCard()
    {
        GameObject attackerObject = new GameObject();
        CardAttack attacker = attackerObject.AddComponent<CardAttack>();
        GameObject enemyObject = new GameObject();
        CardAttack enemy = enemyObject.AddComponent<CardAttack>();

        var attackerText = new GameObject().AddComponent<TMPro.TextMeshProUGUI>();
        attackerText.text = "10";
        attackerText.transform.SetParent(attackerObject.transform);

        var enemyText = new GameObject().AddComponent<TMPro.TextMeshProUGUI>();
        enemyText.text = "1"; 
        enemyText.transform.SetParent(enemyObject.transform);

        enemy.SetLuckPassive(100); 

        attacker.Attack(attacker, enemy);

        TMPro.TextMeshProUGUI[] enemyStats = enemy.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        int enemyHP = int.Parse(enemyStats[1].text);
        Assert.AreEqual(30, enemyHP); 
    }

    [Test]
    public void Relentless_DoublesDamage()
    {
        GameObject attackerObject = new GameObject();
        CardAttack attacker = attackerObject.AddComponent<CardAttack>();
        GameObject enemyObject = new GameObject();
        CardAttack enemy = enemyObject.AddComponent<CardAttack>();

        var attackerText = new GameObject().AddComponent<TMPro.TextMeshProUGUI>();
        attackerText.text = "10"; // Attaque de 10
        attackerText.transform.SetParent(attackerObject.transform);

        var enemyText = new GameObject().AddComponent<TMPro.TextMeshProUGUI>();
        enemyText.text = "30"; // HP de 30
        enemyText.transform.SetParent(enemyObject.transform);

        attacker.SetLuckPassive(100);

        attacker.Attack(attacker, enemy);

        TMPro.TextMeshProUGUI[] enemyStats = enemy.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        int enemyHP = int.Parse(enemyStats[1].text);
        Assert.AreEqual(10, enemyHP); // 30 - (10 * 2) = 10
    }
}