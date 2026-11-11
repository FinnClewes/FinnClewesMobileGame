using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    [SerializeField] TMP_Text healthText;
    [SerializeField] SpriteRenderer sprite;   // for hit flash
    [SerializeField] TMP_Text intentText;
    EnemyPool pool;

    int damage = 2;
    int coinReward = 25;
    string displayName = "Enemy";
    int turnCounter;
    int nextAttack;

    public int Health { get; private set; }
    public int MaxHealth => maxHealth;
    public int Damage => damage;
    public int CoinReward => coinReward;
    public string DisplayName => displayName;
    public int NextAttack => nextAttack;
    public bool NextIsHeavy { get; private set; }

    public Enemy Init(EnemyPool p) { pool = p; return this; }

    void OnEnable()
    {
        Health = maxHealth;
        if (sprite) sprite.color = Color.white;   // clear any hit flash
        RefreshText();
    }

    public bool TakeDamage(int amount)            // returns true if it died
    {
        Health = Mathf.Max(0, Health - amount);
        RefreshText();
        return Health == 0;
    }

    void RefreshText()
    {
        if (healthText) healthText.text = Health + " / " + maxHealth;
    }

    public void Despawn()
    {
        pool.Release(this);
    }

    public void Setup(EnemyType t, float scale)
    {
        displayName = t.displayName;

        maxHealth = Mathf.RoundToInt(t.maxHealth * scale);
        damage = Mathf.RoundToInt(t.damage * scale);
        coinReward = Mathf.RoundToInt(t.coinReward * scale);
        if (sprite && t.sprite) sprite.sprite = t.sprite;

        Health = maxHealth;
        RefreshText();

        turnCounter = 0;
        PlanNextMove();
    }

    public void PlanNextMove()
    {
        turnCounter++;
        NextIsHeavy = turnCounter % 3 == 0;
        nextAttack = NextIsHeavy ? damage * 2 : damage;

        if (intentText)
        {
            intentText.text = NextIsHeavy ? $"HEAVY: {nextAttack}" : $"Attack: {nextAttack}";
            intentText.color = NextIsHeavy ? Color.red : Color.white;
        }
    }
}
