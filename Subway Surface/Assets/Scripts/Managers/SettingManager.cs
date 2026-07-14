using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager singleton;
    private void Awake() { singleton = this; }

    public float swipeThreshold = 100f;
    public float musicSoundVolume = 0.8f;
    public float soundFxVolume = 0.8f;

    private void Start()
    {
        LoadData();
    }

    public void LoadData() {
        SettingsData settingsData = new SettingsData();
        if (SaveLoadManager.CheckFileExists(SaveLoadManager.singleton.currencyDataFileName))
            SaveLoadManager.singleton.Load(settingsData, SaveLoadManager.singleton.currencyDataFileName);
        else SaveLoadManager.singleton.Save(settingsData, SaveLoadManager.singleton.currencyDataFileName);

        musicSoundVolume = settingsData.musicSoundVolume;
        soundFxVolume = settingsData.SoundFxVolume;
    }

    public void SaveData()
    {
        SettingsData settingsData = new SettingsData();
        settingsData.SoundFxVolume = soundFxVolume;
        settingsData.musicSoundVolume = musicSoundVolume;
        SaveLoadManager.singleton.Save(settingsData, SaveLoadManager.singleton.currencyDataFileName);
    }
}
