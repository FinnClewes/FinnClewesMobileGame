using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

/// <summary>
/// Spawns 200 cubes from an object pool and moves them all together
/// from ONE Update loop (cheaper than 200 separate Update() calls).
///
/// Setup:
///  1. Create an empty GameObject, add this script.
///  2. (Optional) assign a cube prefab. If left empty, primitive cubes are used.
///  3. Press Play.
///
/// Controls (while playing):
///  Space  - release all cubes back to the pool
///  Enter  - take 200 cubes from the pool again (no new allocations)
/// </summary>
public class PooledCubeSwarm : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private GameObject cubePrefab;      // optional
    [SerializeField] private int count = 200;

    [Header("Layout")]
    [SerializeField] private int columns = 20;
    [SerializeField] private float spacing = 1.5f;

    [Header("Motion (shared by every cube)")]
    [SerializeField] private float amplitude = 2f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float waveOffset = 0.25f;   // 0 = perfect unison, >0 = ripple
    [SerializeField] private Vector3 direction = Vector3.up;
    [SerializeField] private float spinSpeed = 45f;

    private ObjectPool<GameObject> pool;
    private readonly List<GameObject> active = new List<GameObject>();
    private readonly List<Vector3> basePositions = new List<Vector3>();

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: CreateCube,
            actionOnGet: c => c.SetActive(true),
            actionOnRelease: c => c.SetActive(false),
            actionOnDestroy: Destroy,
            collectionCheck: false,
            defaultCapacity: count,
            maxSize: count);
    }

    private void Start() => SpawnAll();

    private GameObject CreateCube()
    {
        GameObject go = cubePrefab != null
            ? Instantiate(cubePrefab, transform)
            : GameObject.CreatePrimitive(PrimitiveType.Cube);

        go.transform.SetParent(transform, false);
        go.transform.localScale = Vector3.one * 0.5f;
        return go;
    }

    private void SpawnAll()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject cube = pool.Get();

            int row = i / columns;
            int col = i % columns;
            Vector3 pos = new Vector3(
                (col - columns * 0.5f) * spacing,
                0f,
                row * spacing);

            cube.transform.position = transform.position + pos;

            active.Add(cube);
            basePositions.Add(cube.transform.position);
        }
    }

    private void ReleaseAll()
    {
        for (int i = active.Count - 1; i >= 0; i--)
            pool.Release(active[i]);

        active.Clear();
        basePositions.Clear();
    }

    private void Update()
    {
        Keyboard kb = Keyboard.current;
        if (kb != null)
        {
            if (kb.spaceKey.wasPressedThisFrame) ReleaseAll();
            if (kb.enterKey.wasPressedThisFrame && active.Count == 0) SpawnAll();
        }

        float t = Time.time * speed;
        Vector3 dir = direction.normalized;

        for (int i = 0; i < active.Count; i++)
        {
            // Same wave for every cube; waveOffset shifts phase per index
            float wave = Mathf.Sin(t + i * waveOffset) * amplitude;

            Transform tr = active[i].transform;
            tr.position = basePositions[i] + dir * wave;
            tr.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.World);
        }
    }
}