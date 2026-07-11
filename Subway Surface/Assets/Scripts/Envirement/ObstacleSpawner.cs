using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] List<Transform> obsticleSpawnPoints;
    [SerializeField] List<GameObject> obsticlePrefabs;
    [SerializeField] List<GameObject> BlockerObsticlePrefabs;
    [SerializeField] float spawnInterval;

    private void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        SpawnPattern2();

        yield return new WaitForSeconds(spawnInterval);
        StartCoroutine(SpawnObstacle());
    }

    private void SpawnPattern1() {
        int posIndex = Random.Range(0, obsticleSpawnPoints.Count - 1);
        Instantiate(obsticlePrefabs[0], obsticleSpawnPoints[posIndex].position, Quaternion.identity);
    }

    private void SpawnPattern2()
    {
        int posIndex = Random.Range(0, obsticleSpawnPoints.Count - 1);
        int posIndex2 = Random.Range(0, obsticleSpawnPoints.Count - 1);
        Instantiate(obsticlePrefabs[0], obsticleSpawnPoints[posIndex].position, Quaternion.identity);
        if (posIndex != posIndex2) 
            Instantiate(obsticlePrefabs[1], obsticleSpawnPoints[posIndex2].position, Quaternion.identity);
    }

    private void SpawnPattern3()
    {
        int posIndex = Random.Range(0, obsticleSpawnPoints.Count - 1);
        Instantiate(obsticlePrefabs[0], obsticleSpawnPoints[posIndex].position, Quaternion.identity);
    }
}
