using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] Enemy prefab;
    [SerializeField] int size = 3;            // enemies per level (your slice)
    readonly Stack<Enemy> free = new();

    void Awake()
    {
        for (int i = 0; i < size; i++)
        {
            var e = Instantiate(prefab, transform).Init(this);
            e.gameObject.SetActive(false);
            free.Push(e);
        }
    }

    public Enemy Spawn(Vector3 pos)
    {
        if (free.Count == 0) { Debug.LogWarning("EnemyPool empty"); return null; }
        var e = free.Pop();
        e.transform.position = pos;
        e.gameObject.SetActive(true);
        return e;
    }

    public void Release(Enemy e)
    {
        e.gameObject.SetActive(false);
        free.Push(e);
    }
}
