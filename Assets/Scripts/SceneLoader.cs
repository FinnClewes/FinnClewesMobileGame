using UnityEngine;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour // persistent, see Awake
{
    public static SceneLoader Instance { get; private set; }
    [SerializeField] Slider bar;
    [SerializeField] GameObject loadingScreen;
    void Awake()
    {
        if (Instance  != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public async Awaitable Load(string scene, CancellationToken ct)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();

        loadingScreen.SetActive(true);
        var op = SceneManager.LoadSceneAsync(scene);
        op.allowSceneActivation = false;  // progress now stops at 0.9
        while (op.progress < 0.9f)        // NOT while (!op.isDone)
        {
            bar.value = op.progress / 0.9f;
            await Awaitable.NextFrameAsync(ct);
        }
        op.allowSceneActivation = true;
        await op;                         // completes on activation
        loadingScreen.SetActive(false);

        Debug.Log($"[perf] scene {scene} loaded in {sw.ElapsedMilliseconds} ms");
    }
}
