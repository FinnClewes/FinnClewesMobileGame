using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextScale : MonoBehaviour
{
    const string Key = "textScale";
    public static event System.Action Changed;

    public const float Small = 0.85f;
    public const float Normal = 1f;
    public const float Large = 1.25f;

    public static float Factor
    {
        get => PlayerPrefs.GetFloat(Key, 1f);
        set 
        { 
            PlayerPrefs.SetFloat(Key, value); 
            PlayerPrefs.Save(); 
            Changed?.Invoke(); 
        }
    }

    TMP_Text _text;
    float _baseSize;

    void Awake() 
    { 
        _text = GetComponent<TMP_Text>(); 
        _baseSize = _text.fontSize; 
    }

    void OnEnable() { 
        Changed += Apply; 
        Apply(); 
    }

    void OnDisable() {
        Changed -= Apply; 
    }

    void Apply() {
        _text.fontSize = _baseSize * Factor; 
    }
}

