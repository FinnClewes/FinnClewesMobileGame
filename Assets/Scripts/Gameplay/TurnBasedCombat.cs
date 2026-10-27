using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System;
using System.Threading.Tasks;

public class TurnBasedCombat : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] int playerHealth = 10;
    [SerializeField] int maxPlayerHealth = 10;

    [SerializeField] int playerDamage = 2;
    [SerializeField] int enemyDamage = 2;

    [SerializeField] TextMeshProUGUI playerHealthText;
    [SerializeField] TextMeshProUGUI enemyHealthText;
    [SerializeField] Button attackButton;

    [SerializeField] EncounterSpawner spawner;

    Enemy currentEnemy;
    GameObject enemy;

    public static bool result;

    private bool playerTurn = true;

    private Vector2 playerStartPosition;
    private Vector2 enemyStartPosition;
    CancellationToken ct;

    async void Start()
    {
        ct = destroyCancellationToken;
        playerStartPosition = player.transform.position;
        attackButton.onClick.AddListener(PlayerAttack);
        try { await NextEnemy(); }
        catch (OperationCanceledException) { }
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
            UpdateUI();

            if (dead)
            {
                currentEnemy.Despawn();
                if (!spawner.HasMore)
                {
                    EndFight(true); 
                    return;
                }
                await NextEnemy();
                return;
            }

            await Awaitable.WaitForSecondsAsync(1f, ct);

            await DoAttack(enemy, player);
            playerHealth = Mathf.Max(0, playerHealth - enemyDamage);
            UpdateUI();

            if (playerHealth == 0)
            {
                EndFight(false);
                return;
            }

            playerTurn = true;
        }

        catch (OperationCanceledException) { }
    }

    void EndFight(bool won)
    {
        Debug.Log(won ? "Enemy dead" : "Player dead");
        attackButton.interactable = false;
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
        if (currentEnemy != null)
        {
            enemyHealthText.text = currentEnemy.Health + " / " + currentEnemy.MaxHealth;
        }
    }
}
