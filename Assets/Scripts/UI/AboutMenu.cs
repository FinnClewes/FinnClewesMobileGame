using UnityEngine;
using UnityEngine.InputSystem;

public class AboutMenu : MonoBehaviour
{
    [SerializeField] GameObject aboutPanel;

    void Start() => aboutPanel.SetActive(false);    // hidden when the menu loads

    public void OnAboutPressed() => aboutPanel.SetActive(true);
    public void OnClosePressed() => aboutPanel.SetActive(false);

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null || !kb.escapeKey.wasPressedThisFrame) return;
        if (aboutPanel.activeSelf) aboutPanel.SetActive(false);   // Android back closes it
    }
}