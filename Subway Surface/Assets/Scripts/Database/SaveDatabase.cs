using System;

[Serializable]
public class CurrencyData
{
    public int coinAmount;
    public int highScore;

    public CurrencyData()
    {
        coinAmount = 0;
        highScore = 0;
    }
}

[Serializable]
public class SkinData
{
    public SkinData() { }
}

[Serializable]
public class SettingsData
{
    public float musicSoundVolume;
    public float SoundFxVolume;

    public SettingsData()
    {
        musicSoundVolume = 0.8f;
        SoundFxVolume = 0.8f;
    }
}