using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WalletHud : MonoBehaviour
{
    [SerializeField] TMP_Text coinsLabel;
    [SerializeField] TMP_Text upgradeLabel;
    [SerializeField] Button upgradeButton;
    [SerializeField] TMP_Text healthUpgradeLabel;
    [SerializeField] Button healthUpgradeButton;
    Wallet wallet;
    private void Awake()
    {
        wallet = Wallet.Instance;
    }

    private void Start()
    {
        upgradeButton.onClick.AddListener(OnUpgradeClicked);
        healthUpgradeButton.onClick.AddListener(OnHealthUpgradeClicked);
    }

    void OnUpgradeClicked()
    {
        if (!wallet.TryUpgrade())
            Debug.Log("Not enough coins");
    }

    void OnHealthUpgradeClicked()
    {
        if (!wallet.TryUpgradeHealth())
            Debug.Log("Not enough coins");
    }

    void OnEnable() { wallet.Changed += Refresh; Refresh(); }

    void OnDisable() { wallet.Changed -= Refresh; }

    void Refresh()
    {
        coinsLabel.SetText("{0}", wallet.Coins);            // no string concat
        upgradeLabel.SetText("Upgrade ({0})", wallet.NextCost);
        upgradeButton.interactable = wallet.Coins >= wallet.NextCost;
        healthUpgradeLabel.SetText("Health ({0})", wallet.NextHealthCost);
        healthUpgradeButton.interactable = wallet.Coins >= wallet.NextHealthCost;
    }

    public void ResetWallet()
    {
        PlayerPrefs.DeleteAll();
        wallet.Restore(0, 0);
    }
}
