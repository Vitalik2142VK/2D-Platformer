using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    private const string IsDying = nameof(IsDying);

    [SerializeField] private EnemyMover _enemyMover;
    [SerializeField] private MeleeAttack _attack;
    [SerializeField] private PlayerFinder _playerSearchArea;
    [SerializeField, Min(0)] private float _timeDelete = 5;

    private int _hashIsDying = Animator.StringToHash(IsDying);

    private void OnEnable()
    {
        _playerSearchArea.PlayerFound += ChangeMove;

        Health.Over += Remove;
    }

    private void Update()
    {
        if (Health.IsAlive)
        {
            _enemyMover.Fall();
            _enemyMover.Move();

            Attack();
        } 
    }

    private void OnDisable()
    {
        _playerSearchArea.PlayerFound -= ChangeMove;

        Health.Over -= Remove;
    }

    private void Attack()
    {
        if (_attack.IsFoundTargets())
            _attack.Attack();
    }

    private void ChangeMove(Player player, bool isPlayingFound)
    {
        _enemyMover.ChangeMoveToPlayer(player, isPlayingFound);
    }

    private void Remove()
    {
        if (TryGetComponent(out MeleeAttack attack))
            Destroy(attack);

        Destroy(_enemyMover);

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger(_hashIsDying);

        Die();
    }

    private IEnumerator RemoveAfterWhile()
    {
        yield return new WaitForSeconds(_timeDelete);

        Destroy(gameObject);
    }

    protected override void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;

        StartCoroutine(RemoveAfterWhile());
    }
}
