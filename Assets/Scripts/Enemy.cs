using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    [SerializeField] TMP_Text healthText;
    [SerializeField] SpriteRenderer sprite;   // optional, for hit flash
    EnemyPool pool;

    public int Health { get; private set; }
    public int MaxHealth => maxHealth;

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

    public void Despawn() => pool.Release(this);
}
