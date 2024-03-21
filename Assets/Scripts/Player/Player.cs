using System;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private MeleeAttack _attack;

    public event Action CoinTaken;

    private void OnEnable()
    {
        Health.Over += Die;
    }

    private void Update()
    {
        if (Health.IsAlive)
        {
            _playerMover.Fall();
            _playerMover.Jump();
            _playerMover.Move();
            Attack();
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
        Health.Over -= Die;
    }

    private void Attack()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _attack.IsFoundTargets();
            _attack.Attack();
        }
    }

    protected override void Die()
    {
        Debug.Log("You dead");
    }
}
