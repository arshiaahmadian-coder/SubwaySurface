using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] List<Transform> obsticleSpawnPoints;
    [SerializeField] Transform obsticleSpawnPointsParent;
    [SerializeField] List<GameObject> obsticlePrefabs;
    [SerializeField] List<GameObject> BlockerObsticlePrefabs;
    [SerializeField] float spawnInterval;
    
    [Header("before start spawns")]
    [SerializeField] float spawnDistance;
    [SerializeField] int spawnAmount;

    private bool spawn = true;

    private void Start()
    {
        for (int i = 0; i <= spawnAmount; i++)
        {
            SpawnPattern2();
            obsticleSpawnPointsParent.Translate(0, 0, spawnDistance);
        }

        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        if (ChunkManager.singleton.chunkMoveSpeed < -12) SpawnPattern3();
        else SpawnPattern2();

        yield return new WaitForSeconds(spawnInterval);
        if (spawn) StartCoroutine(SpawnObstacle());
        ChunkManager.singleton.IncreaseSpeed();
    }

    private void SpawnPattern1() {
        int posIndex = Random.Range(0, obsticleSpawnPoints.Count);
        int obsIndex = Random.Range(0, obsticlePrefabs.Count);
        Instantiate(obsticlePrefabs[obsIndex], obsticleSpawnPoints[posIndex].position, Quaternion.identity);
    }

    private void SpawnPattern2()
    {
        int posIndex = Random.Range(0, obsticleSpawnPoints.Count);
        int posIndex2 = Random.Range(0, obsticleSpawnPoints.Count);
        int obsIndex = Random.Range(0, obsticlePrefabs.Count);
        int blkIndex = Random.Range(0, BlockerObsticlePrefabs.Count);
        Instantiate(obsticlePrefabs[obsIndex], obsticleSpawnPoints[posIndex].position, Quaternion.identity);
        if (posIndex != posIndex2) 
            Instantiate(BlockerObsticlePrefabs[blkIndex], obsticleSpawnPoints[posIndex2].position, Quaternion.identity);
    }

    private void SpawnPattern3()
    {
        int posIndex = Random.Range(0, obsticleSpawnPoints.Count);
        int posIndex2 = Random.Range(0, obsticleSpawnPoints.Count);
        int posIndex3;
        do { posIndex3 = Random.Range(0, obsticleSpawnPoints.Count); } 
        while(posIndex3 == posIndex2 || posIndex3 == posIndex);

        int obsIndex = Random.Range(0, obsticlePrefabs.Count);
        int blkIndex = Random.Range(0, BlockerObsticlePrefabs.Count);
        Instantiate(obsticlePrefabs[obsIndex], obsticleSpawnPoints[posIndex].position, Quaternion.identity);
        if (posIndex != posIndex2) 
            Instantiate(BlockerObsticlePrefabs[blkIndex], obsticleSpawnPoints[posIndex2].position, Quaternion.identity);
        blkIndex = Random.Range(0, BlockerObsticlePrefabs.Count);
        Instantiate(BlockerObsticlePrefabs[blkIndex], obsticleSpawnPoints[posIndex3].position, Quaternion.identity);
    }

    public void Stop()
    {
        spawn = false;
    }
}
