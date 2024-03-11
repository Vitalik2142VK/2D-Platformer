using UnityEngine;

public class CoinsSpawn : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _pointsSpawn;

    private void Start()
    {
        _coinPrefab.SetPlayer(_player);

        for (int i = 0; i < _pointsSpawn.childCount; i++)
        {
            SpawnCoin(_pointsSpawn.GetChild(i));
        }
    }

    private void SpawnCoin(Transform pointSpawn)
    {
        Instantiate(_coinPrefab, pointSpawn.position, pointSpawn.rotation);
    }
}
