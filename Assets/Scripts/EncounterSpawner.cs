using UnityEngine;

public class EncounterSpawner : MonoBehaviour
{
    [SerializeField] EnemyPool pool;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnDelay = 0.5f;
    [SerializeField] EnemyType[] normalEnemies;
    [SerializeField] EnemyType boss;
    [SerializeField] int enemiesPerWave = 3;
    [SerializeField] int bossEvery = 3;
    [SerializeField] float scalePerWave = 0.2f;

    public int Wave { get; private set; } = 1;
    int spawnedThisWave;

    public bool IsBossWave => Wave % bossEvery == 0;
    public bool HasMoreInWave => spawnedThisWave < (IsBossWave ? 1 : enemiesPerWave);
    float Scale => 1f + scalePerWave * (Wave - 1);

    public void StartNextWave() 
    { 
        Wave++; spawnedThisWave = 0; 
    }

    public async Awaitable<Enemy> SpawnNextAsync()
    {
        await Awaitable.WaitForSecondsAsync(spawnDelay, destroyCancellationToken);
        var enemy = pool.Spawn(spawnPoint.position);
        if (enemy == null) return null;
        EnemyType type = IsBossWave ? boss : normalEnemies[Random.Range(0, normalEnemies.Length)];
        enemy.Setup(type, Scale);
        spawnedThisWave++;
        return enemy;
    }
}