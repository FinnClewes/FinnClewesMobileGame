using UnityEngine;

public class EncounterSpawner : MonoBehaviour
{
    [SerializeField] EnemyPool pool;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int enemiesInLevel = 3;
    [SerializeField] float spawnDelay = 0.5f;
    [SerializeField] EnemyType[] wave;
    int spawned;

    public bool HasMore => spawned < wave.Length;

    public async Awaitable<Enemy> SpawnNextAsync()
    {
        await Awaitable.WaitForSecondsAsync(spawnDelay, destroyCancellationToken);
        var enemy = pool.Spawn(spawnPoint.position);
        if (enemy == null) return null;
        enemy.Setup(wave[spawned]);
        spawned++;
        return enemy;
    }
}