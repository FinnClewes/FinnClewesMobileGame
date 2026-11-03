using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WalletHud : MonoBehaviour
{
    [SerializeField] TMP_Text coinsLabel;
    [SerializeField] TMP_Text upgradeLabel;
    [SerializeField] Button upgradeButton;
    Wallet wallet;
    private void Awake()
    {
        wallet = Wallet.Instance;
    }

    private void Start()
    {
        upgradeButton.onClick.AddListener(OnUpgradeClicked);
    }

    void OnUpgradeClicked()
    {
        if (!wallet.TryUpgrade())
            Debug.Log("Not enough coins");
    }

    void OnEnable() { wallet.Changed += Refresh; Refresh(); }

    void OnDisable() { wallet.Changed -= Refresh; }

    void Refresh()
    {
        coinsLabel.SetText("{0}", wallet.Coins);            // no string concat
        upgradeLabel.SetText("Upgrade ({0})", wallet.NextCost);
        upgradeButton.interactable = wallet.Coins >= wallet.NextCost;
    }

    public void ResetWallet()
    {
        PlayerPrefs.DeleteAll();
        wallet.Restore(0, 0);
    }
}
