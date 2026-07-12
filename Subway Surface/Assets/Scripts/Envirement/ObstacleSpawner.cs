using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] List<Transform> obsticleSpawnPoints;
    [SerializeField] List<GameObject> obsticlePrefabs;
    [SerializeField] List<GameObject> BlockerObsticlePrefabs;
    [SerializeField] float spawnInterval;
    private bool spawn = true;

    private void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        SpawnPattern3();

        yield return new WaitForSeconds(spawnInterval);
        if (spawn) StartCoroutine(SpawnObstacle());
    }

    private void SpawnPattern1() {
        int posIndex = Random.Range(0, obsticleSpawnPoints.Count - 1);
        int obsIndex = Random.Range(0, obsticlePrefabs.Count - 1);
        Instantiate(obsticlePrefabs[obsIndex], obsticleSpawnPoints[posIndex].position, Quaternion.identity);
    }

    private void SpawnPattern2()
    {
        int posIndex = Random.Range(0, obsticleSpawnPoints.Count - 1);
        int posIndex2 = Random.Range(0, obsticleSpawnPoints.Count - 1);
        int obsIndex = Random.Range(0, obsticlePrefabs.Count - 1);
        int blkIndex = Random.Range(0, BlockerObsticlePrefabs.Count - 1);
        Instantiate(obsticlePrefabs[obsIndex], obsticleSpawnPoints[posIndex].position, Quaternion.identity);
        if (posIndex != posIndex2) 
            Instantiate(BlockerObsticlePrefabs[blkIndex], obsticleSpawnPoints[posIndex2].position, Quaternion.identity);
    }

    private void SpawnPattern3()
    {
        int posIndex = Random.Range(0, obsticleSpawnPoints.Count - 1);
        int posIndex2 = Random.Range(0, obsticleSpawnPoints.Count - 1);
        int posIndex3 = Random.Range(0, obsticleSpawnPoints.Count - 1);
        int obsIndex = Random.Range(0, obsticlePrefabs.Count - 1);
        int blkIndex = Random.Range(0, BlockerObsticlePrefabs.Count - 1);
        Instantiate(obsticlePrefabs[obsIndex], obsticleSpawnPoints[posIndex].position, Quaternion.identity);
        if (posIndex != posIndex2) 
            Instantiate(BlockerObsticlePrefabs[blkIndex], obsticleSpawnPoints[posIndex2].position, Quaternion.identity);
        blkIndex = Random.Range(0, BlockerObsticlePrefabs.Count - 1);
        if (posIndex3 != posIndex2 && posIndex3 != posIndex)
            Instantiate(BlockerObsticlePrefabs[blkIndex], obsticleSpawnPoints[posIndex2].position, Quaternion.identity);
    }

    public void Stop()
    {
        spawn = false;
    }
}
