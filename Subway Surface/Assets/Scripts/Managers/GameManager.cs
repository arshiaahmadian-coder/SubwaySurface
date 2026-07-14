using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] GameObject UIBox;
    [SerializeField] TMP_Text UiScoreTxt;
    [SerializeField] TMP_Text scoreTxt;
    [SerializeField] TMP_Text coinTxt;
    public int scoreAmount;
    private bool isGameOver;
    private float timer;
    public static GameManager singleton;
    private void Awake() { singleton = this; }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.1f && !isGameOver)
        {
            timer = 0;
            scoreAmount += -1 * ((int) (ChunkManager.singleton.chunkMoveSpeed * 0.1f));
            UiScoreTxt.text = scoreAmount.ToString();
        } 
    }

    public void GameOver()
    {
        isGameOver = true;
        StartCoroutine(SoundManager.singleton.StopMusicSlowly());
        ChunkManager.singleton.Stop();
        Invoke(nameof(OpenDialog), 1f);
        // save data
        CurrencyData _currencyData = new CurrencyData();
        if (SaveLoadManager.CheckFileExists(SaveLoadManager.singleton.currencyDataFileName))
            SaveLoadManager.singleton.Load(_currencyData, SaveLoadManager.singleton.currencyDataFileName);

        _currencyData.coinAmount += CoinCounter.singleton.coinsAmount;
        _currencyData.highScore = (scoreAmount > _currencyData.highScore) ? scoreAmount : _currencyData.highScore;
        SaveLoadManager.singleton.Save(_currencyData, SaveLoadManager.singleton.currencyDataFileName);
    }

    public void OpenDialog()
    {
        // UI
        scoreTxt.text = "Score: " + scoreAmount;
        coinTxt.text = "Coin: " + CoinCounter.singleton.coinsAmount;
        dialogBox.SetActive(true);
        UIBox.SetActive(false);
    }
}
