using UnityEngine;

public class ChunkMove : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] float spawnOffsed;
    [SerializeField] float despawnPos;
    [SerializeField] bool teleport = true;

    private void Update()
    {
        transform.Translate(0, 0, ChunkManager.singleton.chunkMoveSpeed * Time.deltaTime);

        if (transform.position.z <= despawnPos)
        {
            if (teleport)
            {
                // spawn another chunk
                Vector3 newPos = new Vector3(0, 0, transform.position.z + spawnOffsed);
                transform.position = newPos;
            } else // destroy self
                Destroy(gameObject);
        }
    }
}
