using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;

        string guid = Application.buildGUID;
        string build = guid.Length >= 8 ? guid.Substring(0, 8) : guid;

        Telemetry.Log("session_start",
            ("app_version", Application.version),
            ("build", build),
            ("device_model", SystemInfo.deviceModel),
            ("android", SystemInfo.operatingSystem));

        DontDestroyOnLoad(gameObject);
    }
    async Awaitable Start() => _ = SceneLoader.Instance.Load("Menu", Application.exitCancellationToken);
}
