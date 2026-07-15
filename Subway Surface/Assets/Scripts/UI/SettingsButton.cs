using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : CustomButton
{
    public GameObject settingsMenu;
    public GameObject buttonsBox;
    public Slider musicSlider;
    public Slider sfxSlider;

    public override void OnClick()
    {
        base.OnClick();
        settingsMenu.SetActive(true);
        buttonsBox.SetActive(false);

        musicSlider.value = SettingManager.singleton.musicSoundVolume;
        sfxSlider.value = SettingManager.singleton.soundFxVolume;
    }

    public void OnSave() {
        SoundManager.singleton.PlayClickSound();
        settingsMenu.SetActive(false);
        buttonsBox.SetActive(true);

        SettingManager.singleton.musicSoundVolume = musicSlider.value;
        SettingManager.singleton.soundFxVolume = sfxSlider.value;
        SettingManager.singleton.SaveData();
    }

    public void OnCancel() {
        SoundManager.singleton.PlayClickSound();
        settingsMenu.SetActive(false);
        buttonsBox.SetActive(true);

        musicSlider.value = SettingManager.singleton.musicSoundVolume;
        sfxSlider.value = SettingManager.singleton.soundFxVolume;

        SoundManager.singleton.musicAudioSource.volume = musicSlider.value;
        SoundManager.singleton.effectsAudioSource.volume = sfxSlider.value;
    }

    public void RefreshSounds()
    {
        SoundManager.singleton.musicAudioSource.volume = musicSlider.value;
        SoundManager.singleton.effectsAudioSource.volume = sfxSlider.value;
    }
}
