using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public ObstacleSpawner obstacleSpawner;
    public static ChunkManager singleton;
    private void Awake() { singleton = this; }
    public float chunkMoveSpeed;

    public void Stop()
    {
        chunkMoveSpeed = 0;
        obstacleSpawner.Stop();
    }
}
