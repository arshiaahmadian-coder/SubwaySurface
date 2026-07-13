using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public static CoinCounter singleton;
    private void Awake() { singleton = this; }

    public int coinsAmount;
    public TMP_Text coinCounterText;

    public void AddCoin(int amount = 1)
    {
        coinsAmount += amount;
        coinCounterText.text = coinsAmount.ToString();
    }
}
