using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    private void Awake() { singleton = this; }

    public void GameOver()
    {
        StartCoroutine(SoundManager.singleton.StopMusicSlowly());
        ChunkManager.singleton.Stop();
        Invoke(nameof(ReloadScene), 4f);
        // save data
        CurrencyData _currencyData = new CurrencyData();
        if (SaveLoadManager.CheckFileExists(SaveLoadManager.singleton.currencyDataFileName))
            SaveLoadManager.singleton.Load(_currencyData, SaveLoadManager.singleton.currencyDataFileName);
        _currencyData.coinAmount += CoinCounter.singleton.coinsAmount;
            SaveLoadManager.singleton.Save(_currencyData, SaveLoadManager.singleton.currencyDataFileName);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
