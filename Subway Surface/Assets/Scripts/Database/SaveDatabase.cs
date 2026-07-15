using System;
using System.Collections.Generic;

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
    public int selectedSkin;
    public List<int> ownedSkins;
    public SkinData()
    {
        ownedSkins = new List<int>();
        selectedSkin = 0;
        ownedSkins.Add(selectedSkin);
    }
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