using UnityEngine;

public class EncounterSpawner : MonoBehaviour
{
    [SerializeField] EnemyPool pool;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int enemiesInLevel = 3;
    [SerializeField] float spawnDelay = 0.5f;
    int spawned;

    public bool HasMore => spawned < enemiesInLevel;

    public async Awaitable<Enemy> SpawnNextAsync()
    {
        await Awaitable.WaitForSecondsAsync(spawnDelay, destroyCancellationToken);
        spawned++;
        return pool.Spawn(spawnPoint.position);
    }
}