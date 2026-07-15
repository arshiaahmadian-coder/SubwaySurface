using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter singleton;
    private void Awake() { singleton = this; }

    public int coinsAmount;
    public Text coinCounterText;

    public void AddCoin(int amount = 1)
    {
        coinsAmount += amount;
        coinCounterText.text = coinsAmount.ToString();
    }
}
