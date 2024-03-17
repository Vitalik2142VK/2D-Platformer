using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private const string IsMeleeAttack = nameof(IsMeleeAttack);

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _damageLayerMask;
    [SerializeField, Min(0)] private float _damage;
    [SerializeField, Min(0)] private float _attackRange;
    [SerializeField, Min(0)] private float _timeBetweenAttack;

    private Collider2D[] _targets;
    private Animator _animator;
    private float _timer;
    private int _hashIsMeleeAttack = Animator.StringToHash(IsMeleeAttack);

    private void Start()
    {
        AssignComponents();
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    public bool IsFoundTargets()
    {
        _targets = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _damageLayerMask);

        return _targets.Length > 0;
    }

    public void Attack()
    {
        if (_timer <= 0) 
        {
            AttackTargets();
        }
    }

    private void AssignComponents()
    {
        _animator = GetComponent<Animator>();
    }

    private void AttackTargets()
    {
        _animator.SetTrigger(_hashIsMeleeAttack);

        if (_targets.Length > 0)
        {
            foreach (var target in _targets)
            {
                target.GetComponent<Health>().TakeDamage(_damage);
            }
        }

        _timer = _timeBetweenAttack;
    }
}
