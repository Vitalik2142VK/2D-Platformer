using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action CoinIsTaken;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Coin>() != null)
            CoinIsTaken?.Invoke();
    }
}
