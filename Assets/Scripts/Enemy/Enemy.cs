using UnityEngine;

public class Enemy : Character
{
    private const string IsDying = nameof(IsDying);

    [SerializeField] private Mover _enemyMover;
    [SerializeField] private MeleeAttack _attack;

    private int _hashIsDying = Animator.StringToHash(IsDying);

    private void OnEnable()
    {
        Health.HealthOver += Remove;
    }

    private void Update()
    {
        if (Health.IsAlive)
        {
            _enemyMover.Fall();
            _enemyMover.Move();
            _attack.Attack();
        } 
    }

    private void OnDisable()
    {
        Health.HealthOver -= Remove;
    }

    private void Remove()
    {
        if (TryGetComponent(out MeleeAttack attack))
            Destroy(attack);

        Destroy(_enemyMover);

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger(_hashIsDying);

        Health.Remove();
    }
}
