using UnityEngine;

public class WalletPersistence : MonoBehaviour
{
    Wallet wallet;

    void Awake()
    {
        wallet = GetComponent<Wallet>();     // same object, so no Find needed
        WalletSave.Load(wallet);             // load first...
        wallet.Changed += Save;              // ...then start saving on changes
    }

    void OnDestroy() { wallet.Changed -= Save; }

    void OnApplicationPause(bool paused)
    {
        if (paused) WalletSave.Save(wallet);
    }

    void Save() => WalletSave.Save(wallet);
}
