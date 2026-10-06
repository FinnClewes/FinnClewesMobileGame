using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class TurnBasedCombat : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;

    [SerializeField] int playerHealth = 10;
    [SerializeField] int maxPlayerHealth = 10;
    [SerializeField] int enemyHealth = 10;
    [SerializeField] int maxEnemyHealth = 10;

    [SerializeField] TextMeshProUGUI playerHealthText;
    [SerializeField] TextMeshProUGUI enemyHealthText;
    [SerializeField] Button attackButton;

    private bool playerTurn = true;

    private Vector2 playerStartPosition;
    private Vector2 enemyStartPosition;

    private void Start()
    {
        playerStartPosition = player.transform.position;
        enemyStartPosition = enemy.transform.position;

        UpdateUI();

        playerTurn = true;

        attackButton.onClick.AddListener(PlayerAttack);
    }

    void PlayerAttack()
    {
        if (!playerTurn)
        {
            return;
        }

        StartCoroutine(DoAttack(player, enemy, () =>
            {
                enemyHealth -= 2;

                if (enemyHealth <= 0)
                {
                    enemyHealth = 0;
                    UpdateUI();
                    Debug.Log("Enemy defeated");
                    return;
                }

                playerTurn = false;
                UpdateUI();
                Invoke(nameof(EnemyAttack), 1f);
        }));
    }

    void EnemyAttack()
    {
        StartCoroutine(DoAttack(enemy, player, () =>
        {
            playerHealth -= 2;

            if (playerHealth <= 0)
            {
                playerHealth = 0;
                UpdateUI();
                attackButton.interactable = false;
                Debug.Log("player dead");
                return;
            }

            playerTurn = true;
            UpdateUI();
        }));
    }

    IEnumerator DoAttack(GameObject attacker, GameObject target, System.Action onComplete)
    {
        Vector2 attackerStart = (attacker == player) ? playerStartPosition : enemyStartPosition;
        Vector2 targetStart = (target == player) ? playerStartPosition : enemyStartPosition;

        Vector2 attackPos = attackerStart + (targetStart - attackerStart).normalized * 0.5f;
        Vector2 hitPushPos = targetStart + (targetStart - attackerStart).normalized * 0.3f;

        yield return MoveOverTime(attacker, attackerStart, attackPos, 0.1f);

        yield return MoveOverTime(attacker, attackerStart, attackPos, 0.1f);

        yield return MoveOverTime(target, targetStart, hitPushPos, 0.05f);

        yield return MoveOverTime(target, hitPushPos, targetStart, 0.1f);

        onComplete?.Invoke();
    }

    IEnumerator MoveOverTime(GameObject obj, Vector2 startPos, Vector2 endPosition, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            obj.transform.position = Vector2.Lerp(startPos, endPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = endPosition;
    }

    void UpdateUI()
    {
        playerHealthText.text = playerHealth + " / " + maxPlayerHealth;
        enemyHealthText.text = enemyHealth + " / " + maxEnemyHealth;
    }
}
