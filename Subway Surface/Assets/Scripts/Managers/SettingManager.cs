using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager singleton;
    private void Awake() { singleton = this; }

    [HideInInspector]
    public float swipeThreshold = 50f;
    [HideInInspector]
    public float holdThreshold = 0.3f;
    public float musicSoundVolume = 0.8f;
    public float soundFxVolume = 0.8f;

    private void Start()
    {
        LoadData();
        StartCoroutine(SoundManager.singleton.StartMusicSlowly());
    }

    public void LoadData() {
        SettingsData settingsData = new SettingsData();
        if (SaveLoadManager.CheckFileExists(SaveLoadManager.singleton.settingsDataFileName))
            SaveLoadManager.singleton.Load(settingsData, SaveLoadManager.singleton.settingsDataFileName);
        else SaveLoadManager.singleton.Save(settingsData, SaveLoadManager.singleton.settingsDataFileName);

        musicSoundVolume = settingsData.musicSoundVolume;
        soundFxVolume = settingsData.SoundFxVolume;
    }

    public void SaveData()
    {
        SettingsData settingsData = new SettingsData();
        settingsData.SoundFxVolume = soundFxVolume;
        settingsData.musicSoundVolume = musicSoundVolume;
        SaveLoadManager.singleton.Save(settingsData, SaveLoadManager.singleton.settingsDataFileName);
    }
}
