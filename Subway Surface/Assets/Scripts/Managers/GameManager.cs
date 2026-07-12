using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    private void Awake() { singleton = this; }

    public void GameOver()
    {
        print("Player died");
        ChunkManager.singleton.Stop();
    }
}
