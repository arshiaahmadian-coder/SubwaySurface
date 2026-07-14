using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public TMP_Text coinText;
    public TMP_Text highScoreText;

    private void Start()
    {
        CurrencyData _currencyData = new CurrencyData();
        if (SaveLoadManager.CheckFileExists(SaveLoadManager.singleton.currencyDataFileName))
        {
            SaveLoadManager.singleton.Load(_currencyData, SaveLoadManager.singleton.currencyDataFileName);
            coinText.text = _currencyData.coinAmount.ToString();
            highScoreText.text = "High Score: " + _currencyData.highScore;
        } else
        {
            SaveLoadManager.singleton.Save(_currencyData, SaveLoadManager.singleton.currencyDataFileName);
        }
    }
}
