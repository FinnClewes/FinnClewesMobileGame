using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    [SerializeField] TMP_Text healthText;
    [SerializeField] SpriteRenderer sprite;   // for hit flash   
    EnemyPool pool;

    int damage = 2;
    int coinReward = 25;
    string displayName = "Enemy";

    public int Health { get; private set; }
    public int MaxHealth => maxHealth;
    public int Damage => damage;
    public int CoinReward => coinReward;
    public string DisplayName => displayName;

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

    public void Setup(EnemyType t)
    {
        displayName = t.displayName;
        maxHealth = t.maxHealth;
        damage = t.damage;
        coinReward = t.coinReward;
        if (sprite && t.sprite) sprite.sprite = t.sprite;

        Health = maxHealth;
        RefreshText();
    }
}
