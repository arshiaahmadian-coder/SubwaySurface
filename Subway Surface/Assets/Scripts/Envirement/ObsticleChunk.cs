using System.Collections.Generic;
using UnityEngine;

public class ObsticleChunk : MonoBehaviour
{
    [SerializeField] List<Transform> obsticleSpawnPoints;
    [SerializeField] List<GameObject> obsticlePrefabs;
    [SerializeField] List<GameObject> BlickerObsticlePrefabs;

    //   4   5   6   7
    //   0   1   2   3

    private void Start()
    {
        SpawnObstacles();
    }

    void SpawnObstacles()
    {
        int index = Random.Range(0, obsticleSpawnPoints.Count - 1);
        Instantiate(obsticlePrefabs[0], obsticleSpawnPoints[index].position, Quaternion.identity, this.transform);
    }
}