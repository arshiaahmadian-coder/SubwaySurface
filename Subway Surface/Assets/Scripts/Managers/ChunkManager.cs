using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager singleton;
    private void Awake() { singleton = this; }

    public float chunkMoveSpeed;
    public int maxObsticleInChunk { get; private set; }
    public int lastSafeLane = 1;

    public int GetNextSafeLane()
    {
        return lastSafeLane;
    }

    public void SetSafeLane(int lane)
    {
        lastSafeLane = lane;
    }
    public GameObject chunkPrefab;
    public int startChunksAmount;
    public Transform chunkSpawnPoint;

    private void Start()
    {
        for (int i = 0; i < startChunksAmount; i++)
        {
            Vector3 pos =  new Vector3(chunkSpawnPoint.position.x, chunkSpawnPoint.position.y,
                chunkSpawnPoint.position.z + (i * 8)
            );
            Instantiate(chunkPrefab, pos, Quaternion.identity);
        }
    }
}
