
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource effectsAudioSource;
    public AudioSource musicAudioSource;
    public static SoundManager singleton;
    private void Awake() { singleton = this; }
    
    public void PlaySoundEffect(AudioClip clip, float pitchChangeRatio = 0.05f)
    {
        effectsAudioSource.pitch = Random.Range(1 - pitchChangeRatio, 1 + pitchChangeRatio);
        effectsAudioSource.PlayOneShot(clip);
    }
}
