using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] private List<Transform> obstacleSpawnPoints;
    [SerializeField] private Transform spawnPointsParent;

    [Header("Prefabs")]
    [SerializeField] private List<GameObject> obstaclePrefabs;
    [SerializeField] private List<GameObject> blockerPrefabs;
    [SerializeField] private GameObject coinPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 2f;

    [Header("Before Start Spawns")]
    [SerializeField] private float spawnDistance = 20f;
    [SerializeField] private int spawnAmount = 10;

    private bool canSpawn = true;

    private void Start()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            SpawnPatternEasy();
            spawnPointsParent.Translate(Vector3.forward * spawnDistance);
        }

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            if (GameManager.singleton.scoreAmount > 1500)
                SpawnPatternMedium();
            else
                SpawnPatternEasy();

            ChunkManager.singleton.IncreaseSpeed();

            yield return new WaitForSeconds(spawnInterval - (ChunkManager.singleton.chunkMoveSpeed * -0.01f));
        }
    }

    private void SpawnPatternEasy()
    {
        List<int> indexes = GetUniqueIndexes(3);

        SpawnObstacle(indexes[0]);
        SpawnBlocker(indexes[1]);
        SpawnCoin(indexes[2]);
    }

    private void SpawnPatternMedium()
    {
        List<int> indexes = GetUniqueIndexes(4);

        SpawnObstacle(indexes[0]);
        SpawnBlocker(indexes[1]);
        SpawnObstacle(indexes[2]);
        SpawnCoin(indexes[3]);
    }

    private void SpawnObstacle(int spawnPointIndex)
    {
        int prefabIndex = Random.Range(0, obstaclePrefabs.Count);

        Instantiate(
            obstaclePrefabs[prefabIndex],
            obstacleSpawnPoints[spawnPointIndex].position,
            Quaternion.identity);
    }

    private void SpawnBlocker(int spawnPointIndex)
    {
        int prefabIndex = Random.Range(0, blockerPrefabs.Count);

        Instantiate(
            blockerPrefabs[prefabIndex],
            obstacleSpawnPoints[spawnPointIndex].position,
            Quaternion.identity);
    }

    private void SpawnCoin(int spawnPointIndex)
    {
        if (Random.Range(0, 3) != 0) return;

        Instantiate(
            coinPrefab,
            obstacleSpawnPoints[spawnPointIndex].position,
            Quaternion.identity);
    }

    private List<int> GetUniqueIndexes(int count)
    {
        List<int> indexes = new();

        while (indexes.Count < count)
        {
            int index = Random.Range(0, obstacleSpawnPoints.Count);

            if (!indexes.Contains(index))
                indexes.Add(index);
        }

        return indexes;
    }

    public void Stop()
    {
        canSpawn = false;
    }
}