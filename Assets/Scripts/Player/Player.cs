using System;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private MeleeAttack _attack;

    public event Action CoinTaken;

    private new PlayerHealth Health;

    private void Awake()
    {
        Health = (PlayerHealth)base.Health;
    }

    private void OnEnable()
    {
        Health.HealthOver += Die;
    }

    private void Update()
    {
        if (Health.IsAlive)
        {
            _playerMover.Fall();
            _playerMover.Jump();
            _playerMover.Move();
            _attack.Attack();
        }  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            coin.Take();

            CoinTaken?.Invoke();
        } 
        else if (collision.TryGetComponent(out AidKit aidKit))
            Health.RestoreHealth(aidKit.Use());
    }

    private void OnDisable()
    {
        Health.HealthOver -= Die;
    }

    private void Die()
    {
        Health.Remove();
    }
}
