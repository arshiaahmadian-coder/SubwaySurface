
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
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            musicAudioSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeDuration);
            yield return null;
        }

        musicAudioSource.volume = 0f;
        musicAudioSource.Stop();
    }

    public IEnumerator StartMusicSlowly(float fadeDuration = 1f)
    {
        float targetVolume = SettingManager.singleton.musicSoundVolume;
        float startVolume = musicAudioSource.volume;
        float elapsed = 0f;

        musicAudioSource.volume = startVolume;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            musicAudioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / fadeDuration);
            yield return null;
        }

        musicAudioSource.volume = targetVolume;
    }
}
