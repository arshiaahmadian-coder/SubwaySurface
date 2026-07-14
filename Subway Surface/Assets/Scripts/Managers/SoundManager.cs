
using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource effectsAudioSource;
    public AudioSource musicAudioSource;
    public static SoundManager singleton;
    private void Awake() { singleton = this; }
    
    public void PlaySoundEffect(AudioClip clip, float pitchChangeRatio = 0.05f)
    {
        effectsAudioSource.volume = SettingManager.singleton.soundFxVolume;
        effectsAudioSource.pitch = Random.Range(1 - pitchChangeRatio, 1 + pitchChangeRatio);
        effectsAudioSource.PlayOneShot(clip);
    }

    public IEnumerator StopMusicSlowly(float fadeDuration = 1f)
    {
        float startVolume = musicAudioSource.volume;
        while (musicAudioSource.volume > 0)
        {
            musicAudioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        musicAudioSource.volume = 0;
        musicAudioSource.Stop();
    }

    public IEnumerator StartMusicSlowly(float fadeDuration = 1f)
    {
        float startVolume = SettingManager.singleton.musicSoundVolume;
        musicAudioSource.volume = 0;
        while (musicAudioSource.volume <= startVolume)
        {
            musicAudioSource.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        musicAudioSource.volume = startVolume;
    }
}
