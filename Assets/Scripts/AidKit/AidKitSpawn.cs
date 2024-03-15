using System.Collections.Generic;
using UnityEngine;


public class AidKitSpawn : MonoBehaviour
{
    [SerializeField] private AidKit _aidKitPrefab;
    [SerializeField] private Transform _containerSpawnPoints;
    [SerializeField] private int _countAidKit;

    private void Start()
    {
        List<Transform> spawnPoints = new List<Transform>(_containerSpawnPoints.childCount);

        for (int i = 0; i < spawnPoints.Capacity; i++)
        {
            spawnPoints.Add(_containerSpawnPoints.GetChild(i));
        }

        SpawnRandomAidKit(spawnPoints);
    }

    private void SpawnRandomAidKit(List<Transform> spawnPoints)
    {
        int randomIndexPoint;

        for (int i = 0; i < _countAidKit; i++)
        {
            randomIndexPoint = Random.Range(0, spawnPoints.Count);

            SpawnAidKit(spawnPoints[randomIndexPoint]);

            spawnPoints.RemoveAt(randomIndexPoint);
        }
    }

    private void SpawnAidKit(Transform pointSpawn)
    {
        Instantiate(_aidKitPrefab, pointSpawn.position, pointSpawn.rotation);
    }
}
