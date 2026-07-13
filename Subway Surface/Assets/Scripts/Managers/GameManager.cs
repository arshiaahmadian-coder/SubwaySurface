using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    private void Awake() { singleton = this; }

    public void GameOver()
    {
        StartCoroutine(StopMusicSlowly());
        ChunkManager.singleton.Stop();
        Invoke(nameof(ReloadScene), 4f);
    }

    IEnumerator StopMusicSlowly()
    {
        float startVolume = SoundManager.singleton.musicAudioSource.volume;
        float fadeDuration = 1f;
        while (SoundManager.singleton.musicAudioSource.volume > 0)
        {
            SoundManager.singleton.musicAudioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        SoundManager.singleton.musicAudioSource.volume = 0;
        SoundManager.singleton.musicAudioSource.Stop();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
