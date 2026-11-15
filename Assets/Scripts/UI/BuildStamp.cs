using TMPro;
using UnityEngine;

public class BuildStamp : MonoBehaviour
{
    public TMP_Text label;
    
    void Start()
    {
        string guid = Application.buildGUID;
        string id = guid.Length >= 8 ? guid.Substring(0, 8) : guid;
        double hz = Screen.currentResolution.refreshRateRatio.value;
        label.text =
            $"v{Application.version} ({id})\n"
            + $"{SystemInfo.deviceModel}\n"
            + $"{SystemInfo.operatingSystem}\n"
            + $"{SystemInfo.systemMemorySize} MB RAM  "
            + $"{Screen.width}x{Screen.height} @ {hz:0} Hz";
        Debug.Log($"build={Application.version}/{id}");
    }

    public void OpenFeedback() => Application.OpenURL("https://forms.gle/JwW76oGxVzdNWHmm6");
}
