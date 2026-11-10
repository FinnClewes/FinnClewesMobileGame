using UnityEngine;

public class Wallet : MonoBehaviour
{
    public int Coins { get; private set; }
    public int Level { get; private set; }          // damage upgrade level
    public int HealthLevel { get; private set; }    // health upgrade level

    public event System.Action Changed;
    public int NextCost => (int)(100 * Mathf.Pow(1.5f, Level));
    public int NextHealthCost => (int)(100 * Mathf.Pow(1.5f, HealthLevel));
    public static Wallet Instance { get; private set; }
    void Awake() => Instance = this;
    public void Earn(int n) { Coins += n; Changed?.Invoke(); }
    public void Restore(int c, int l, int h = 0)
    { Coins = c; Level = l; HealthLevel = h; Changed?.Invoke(); }

    public bool TryUpgrade()
    {
        int cost = NextCost;
        if (Coins < cost) return false;
        Coins -= cost; Level++;
        Telemetry.Log("upgrade_purchased", ("upgrade_id", "damage"),
                      ("level", Level), ("cost", cost), ("coins_left", Coins));
        Changed?.Invoke();
        return true;
    }

    public bool TryUpgradeHealth()
    {
        int cost = NextHealthCost;
        if (Coins < cost) return false;
        Coins -= cost; HealthLevel++;
        Telemetry.Log("upgrade_purchased", ("upgrade_id", "health"),
                      ("level", HealthLevel), ("cost", cost), ("coins_left", Coins));
        Changed?.Invoke();
        return true;
    }
}
