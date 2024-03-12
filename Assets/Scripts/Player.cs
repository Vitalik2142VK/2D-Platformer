using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;

    public event Action CoinIsTaken;

    private void Update()
    {
        _playerMover.Fall();
        _playerMover.Jump();
        _playerMover.Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            coin.Take();

            CoinIsTaken?.Invoke();
        }      
    }
}
