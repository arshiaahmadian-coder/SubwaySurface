using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public TMP_Text coinText;

    private void Start()
    {
        CurrencyData _currencyData = new CurrencyData();
        if (SaveLoadManager.CheckFileExists(SaveLoadManager.singleton.currencyDataFileName))
        {
            SaveLoadManager.singleton.Load(_currencyData, SaveLoadManager.singleton.currencyDataFileName);
            coinText.text = _currencyData.coinAmount.ToString();
        } else
        {
            coinText.text = "0";
            SaveLoadManager.singleton.Save(_currencyData, SaveLoadManager.singleton.currencyDataFileName);
        }
    }
}
