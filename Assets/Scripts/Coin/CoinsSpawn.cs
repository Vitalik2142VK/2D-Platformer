using UnityEngine;

public class CoinsSpawn : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform _containerSpawnPoints;

    private void Start()
    {
        for (int i = 0; i < _containerSpawnPoints.childCount; i++)
        {
            SpawnCoin(_containerSpawnPoints.GetChild(i));
        }
    }

    private void SpawnCoin(Transform pointSpawn)
    {
        Instantiate(_coinPrefab, pointSpawn.position, pointSpawn.rotation);
    }
}
