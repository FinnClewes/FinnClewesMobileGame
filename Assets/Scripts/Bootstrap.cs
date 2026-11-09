using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
        Telemetry.Log("session_start",
            ("app_version", Application.version),
            ("device_model", SystemInfo.deviceModel),
            ("android", SystemInfo.operatingSystem));
        DontDestroyOnLoad(gameObject);
    }
    async Awaitable Start() => _ = SceneLoader.Instance.Load("Menu", Application.exitCancellationToken);
}
