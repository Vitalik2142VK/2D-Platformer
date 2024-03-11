using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action CoinIsTaken;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            coin.Take();

            CoinIsTaken?.Invoke();
        }      
    }
}
