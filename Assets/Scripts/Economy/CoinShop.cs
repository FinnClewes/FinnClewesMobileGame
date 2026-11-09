using UnityEngine;

public class CoinShop : MonoBehaviour
{
    Wallet wallet;
    IPurchaseProvider store = new FakePurchaseProvider();

    private void Awake()
    {
        wallet = Wallet.Instance;
    }

    public async void OnBuyPack()   // Button handler: async void ok
    {
        if (await store.PurchaseAsync("coins_500")) wallet.Earn(500);
    }
}
