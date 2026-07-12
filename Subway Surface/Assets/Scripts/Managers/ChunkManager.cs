using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public ObstacleSpawner obstacleSpawner;
    public static ChunkManager singleton;
    private void Awake() { singleton = this; }
    public float chunkMoveSpeed;
    public float increaseAmount;

    public void Stop()
    {
        chunkMoveSpeed = 0;
        obstacleSpawner.Stop();
    }

    public void IncreaseSpeed()
    {
        if (chunkMoveSpeed == 0) return;
        
        chunkMoveSpeed += increaseAmount;
    }
}
