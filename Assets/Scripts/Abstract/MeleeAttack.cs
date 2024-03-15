using UnityEngine;

public abstract class MeleeAttack : MonoBehaviour
{
    private const string IsMeleeAttack = nameof(IsMeleeAttack);

    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _damageLayerMask;
    [SerializeField, Min(0)] private float _damage;
    [SerializeField, Min(0)] private float _attackRange;
    [SerializeField, Min(0)] private float _timeBetweenAttack;

    private Animator _animator;
    private float _timer;
    private int _hashIsMeleeAttack = Animator.StringToHash(IsMeleeAttack);

    protected Animator Animator => _animator;
    protected float Damage => _damage;
    protected int HashIsMeleeAttack => _hashIsMeleeAttack;

    private void Start()
    {
        AssignComponents();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    public void Attack()
    {
        if (_timer <= 0) 
        {
            AttackTargets();
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }

    private void AssignComponents()
    {
        _animator = GetComponent<Animator>();
    }
    
    protected Collider2D[] GetEnemies()
    {
        return Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _damageLayerMask);
    }

    protected void UpdateTimer()
    {
        _timer = _timeBetweenAttack;
    }

    protected abstract void AttackTargets();
}
