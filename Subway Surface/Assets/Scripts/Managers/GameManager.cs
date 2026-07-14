using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    private void Awake() { singleton = this; }

    public void GameOver()
    {
        StartCoroutine(SoundManager.singleton.StopMusicSlowly());
        ChunkManager.singleton.Stop();
        Invoke(nameof(ReloadScene), 4f);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
