using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public ObstacleSpawner obstacleSpawner;
    public static ChunkManager singleton;
    private void Awake() { singleton = this; }
    public float chunkMoveSpeed;
    public float chunkMoveSpeedRun;
    public float chunkMoveSpeedSprint;
    public float increaseAmount;

    private void Start()
    {
        chunkMoveSpeed = chunkMoveSpeedRun;
    }

    public void Stop()
    {
        chunkMoveSpeed = 0;
        obstacleSpawner.Stop();
    }

    public void IncreaseSpeed()
    {
        // if (chunkMoveSpeed == 0) return;
        
        // chunkMoveSpeed += increaseAmount;
    }

    public void SprintSpeed()
    {
        if (chunkMoveSpeed == 0) return;
        chunkMoveSpeed = chunkMoveSpeedSprint;
    }

    public void RunSpeed()
    {
        if (chunkMoveSpeed == 0) return;
        chunkMoveSpeed = chunkMoveSpeedRun;
    }
}
