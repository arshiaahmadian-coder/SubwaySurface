using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager singleton;
    private void Awake() { singleton = this; }

    public float chunkMoveSpeed;
}
