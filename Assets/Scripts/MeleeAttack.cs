using UnityEngine;

[RequireComponent (typeof(Animator))]
public class MeleeAttack : MonoBehaviour
{
    private readonly int IsMeleeAttack = Animator.StringToHash(nameof(IsMeleeAttack));

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _damageLayerMask;
    [SerializeField, Min(0)] private float _damage;
    [SerializeField, Min(0)] private float _attackRange;
    [SerializeField, Min(0)] private float _timeBetweenAttack;
    [SerializeField] private bool _isRangeAreaEnabled;

    private Collider2D[] _targets;
    private Animator _animator;
    private Timer _timer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _timer = new Timer(_timeBetweenAttack);
    }

    private void Update()
    {
        _timer.MakeCountdown();
    }

    private void OnDrawGizmosSelected()
    {
        if (_isRangeAreaEnabled)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
        }
    }

    public bool IsFoundTargets()
    {
        _targets = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _damageLayerMask);

        return _targets.Length > 0;
    }

    public void Attack()
    {
        if (_timer.IsTimeUp) 
        {
            AttackTargets();

            _timer.UpdateWaitingTime();
        }
    }

    private void AttackTargets()
    {
        _animator.SetTrigger(IsMeleeAttack);

        if (_targets.Length > 0)
        {
            foreach (var target in _targets)
            {
                target.GetComponent<Health>().TakeDamage(_damage);
            }
        }
    }
}
