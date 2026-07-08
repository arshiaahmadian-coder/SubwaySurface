using UnityEngine;

public class ChunkMove : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] float spawnOffsed;

    private void Update()
    {
        transform.Translate(0, 0, ChunkManager.singleton.chunkMoveSpeed * Time.deltaTime);

        if (transform.position.z <= -50)
        {
            // spawn another chunk
            Vector3 spawnPosition = new Vector3(0, 0, transform.position.z + spawnOffsed);
            Instantiate(chunkPrefab, spawnPosition, Quaternion.identity);
            // destroy self
            Destroy(gameObject);
        }
    }
}
