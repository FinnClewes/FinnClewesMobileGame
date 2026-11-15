using UnityEngine;
using UnityEngine.UI;

public class HighFpsToggle : MonoBehaviour
{
    [SerializeField] Toggle toggle;

    void OnEnable()
    {
        toggle.SetIsOnWithoutNotify(PlayerPrefs.GetInt("perf_mode", 0) == 1);
        toggle.onValueChanged.AddListener(PerformanceMode.Instance.SetHighFps);
    }

    void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(PerformanceMode.Instance.SetHighFps);
    }
}