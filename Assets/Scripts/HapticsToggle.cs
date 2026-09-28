using UnityEngine;
using UnityEngine.UI;

public class HapticsToggle : MonoBehaviour
{
    [SerializeField] Toggle toggle;

    void OnEnable()
    {
        toggle.SetIsOnWithoutNotify(Haptics.Enabled);
        toggle.onValueChanged.AddListener(OnToggled);
    }

    void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(OnToggled);
    }

    void OnToggled(bool value) => Haptics.Enabled = value;
}
