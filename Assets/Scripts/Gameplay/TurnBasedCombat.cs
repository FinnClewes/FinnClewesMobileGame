using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System;
using System.Threading.Tasks;
using UnityEngine.Serialization;

public class TurnBasedCombat : MonoBehaviour
{
    [SerializeField] GameObject player;

    [FormerlySerializedAs("maxPlayerHealth")]
    [SerializeField] int baseMaxHealth = 10;
    [SerializeField] int healthPerLevel = 3;

    int playerHealth;
    int maxPlayerHealth;

    [SerializeField] int basePlayerDamage = 2;
    [SerializeField] int damagePerLevel = 1;

    [SerializeField] int amount = 5;

    [SerializeField] TextMeshProUGUI playerHealthText;
    [SerializeField] Button attackButton;
    [SerializeField] Button defendButton;

    [SerializeField] TextMeshProUGUI waveText;

    [SerializeField] EncounterSpawner spawner;

    [SerializeField] int totalWaves = 0;

    Enemy currentEnemy;
    GameObject enemy;
    Wallet wallet;

    const int LevelId = 1;
    static int attempt;
    float startTime;
    int coinsEarned;
    int kills;

    bool defending;

    public static bool result;

    private bool playerTurn = true;

    private Vector2 playerStartPosition;
    private Vector2 enemyStartPosition;
    CancellationToken ct;

    int playerDamage => basePlayerDamage + damagePerLevel * wallet.Level;
    int MaxHealthFromWallet => baseMaxHealth + healthPerLevel * wallet.HealthLevel;

    private void Awake()
    {
        wallet = Wallet.Instance;
        maxPlayerHealth = MaxHealthFromWallet;
        playerHealth = maxPlayerHealth;
        wallet.Changed += OnWalletChanged;
    }

    void OnDestroy()
    {
        if (wallet != null) wallet.Changed -= OnWalletChanged;
    }

    // Buying a health upgrade mid-fight raises max HP and heals by the same amount.
    void OnWalletChanged()
    {
        int newMax = MaxHealthFromWallet;
        if (newMax == maxPlayerHealth) return;
        playerHealth += newMax - maxPlayerHealth;
        maxPlayerHealth = newMax;
        UpdateUI();
    }

    async void Start()
    {
        ct = destroyCancellationToken;
        playerStartPosition = player.transform.position;
        attackButton.onClick.AddListener(PlayerAttack);
        defendButton.onClick.AddListener(PlayerDefend);
        try { await NextEnemy(); }
        catch (OperationCanceledException) { }
        attempt++;
        startTime = Time.time;
        Telemetry.Log("level_start", ("level_id", LevelId), ("attempt", attempt), ("coins", wallet.Coins));
    }

    async Awaitable NextEnemy()
    {
        playerTurn = false;
        currentEnemy = await spawner.SpawnNextAsync();

        if (currentEnemy == null)
        {
            Debug.LogError("No enemy spawned. Check the EnemyPool size and prefab.");
            return;
        }

        if (spawner.IsBossWave)
        {
            waveText.text = "BOSS";
            waveText.color = Color.red;
        }
        else
        {
            waveText.text = $"Wave {spawner.Wave}";
            waveText.color = Color.white;
        }

        enemy = currentEnemy.gameObject;
        enemyStartPosition = enemy.transform.position;
        UpdateUI();
        playerTurn = true;
    }

    async void PlayerAttack()
    {
        if (!playerTurn) return;
        playerTurn = false;

        try
        {
            await DoAttack(player, enemy);
            bool dead = currentEnemy.TakeDamage(playerDamage);
            Haptics.Pulse();
            UpdateUI();

            if (dead)
            {
                int earn = currentEnemy.CoinReward;
                wallet.Earn(earn);
                coinsEarned += earn;
                kills++;
                currentEnemy.Despawn();
                if (!spawner.HasMoreInWave)
                {
                    if (totalWaves > 0 && spawner.Wave >= totalWaves) 
                    { 
                        EndFight(true); return; }
                        spawner.StartNextWave();
                        playerHealth = Mathf.Min(maxPlayerHealth, playerHealth + maxPlayerHealth / 2);
                    }
                await NextEnemy();
                return;
            }
            await EnemyTurn();
        }

        catch (OperationCanceledException) { }
    }

    async void PlayerDefend()
    {
        if (!playerTurn) return;
        playerTurn = false;
        defending = true;

        try { await EnemyTurn(); }
        catch (OperationCanceledException) { }
    }

    void EndFight(bool won)
    {
        float t = Time.time - startTime;
        if (won)
            Telemetry.Log("level_complete", ("level_id", LevelId), ("time_s", t),
                          ("coins_earned", coinsEarned), ("score", kills));
        else
            Telemetry.Log("level_fail", ("level_id", LevelId), ("time_s", t),
                          ("cause", "hp"), ("coins_earned", coinsEarned));
        wallet.Restore(0, 0);
        Debug.Log(won ? "Enemy dead" : "Player dead");
        attackButton.interactable = false;
        defendButton.interactable = false;
        result = won;
        SceneManager.LoadScene("Result");
    }

    async Awaitable DoAttack(GameObject attacker, GameObject target)
    {
        Vector2 attackerStart = (attacker == player) ? playerStartPosition : enemyStartPosition;
        Vector2 targetStart = (target == player) ? playerStartPosition : enemyStartPosition;

        Vector2 dir = (targetStart - attackerStart).normalized;
        Vector2 attackPos = attackerStart + dir * 0.5f;
        Vector2 hitPushPos = targetStart + dir * 0.3f;

        await MoveOverTime(attacker, attackerStart, attackPos, 0.1f);
        await MoveOverTime(target, targetStart, hitPushPos, 0.05f);
        await MoveOverTime(target, hitPushPos, targetStart, 0.1f);
        await MoveOverTime(attacker, attackPos, attackerStart, 0.1f);
    }

    async Awaitable MoveOverTime(GameObject obj, Vector2 start, Vector2 end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            obj.transform.position = Vector2.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            await Awaitable.NextFrameAsync(ct);
        }

        obj.transform.position = end;
    }

    void UpdateUI()
    {
        playerHealthText.text = playerHealth + " / " + maxPlayerHealth;
    }

    async Awaitable EnemyTurn()
    {
        await Awaitable.WaitForSecondsAsync(1f, ct);

        await DoAttack(enemy, player);
        Haptics.Pulse();
        int hit = defending ? 0 : currentEnemy.NextAttack;
        playerHealth = Mathf.Max(0, playerHealth - hit);
        defending = false;
        currentEnemy.PlanNextMove();
        UpdateUI();

        if (playerHealth == 0)
        {
            EndFight(false);
            return;
        }

        playerTurn = true;
    }
}
