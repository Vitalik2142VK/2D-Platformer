using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Health))]
public class Vampirism : MonoBehaviour
{
    private const KeyCode ActiveVampirism = KeyCode.V;

    [SerializeField] private Transform _pointZone;
    [SerializeField] private LayerMask _layer;
    [SerializeField, Min(1)] private float _radius;
    [SerializeField, Min(1)] private float _timeWork = 6;
    [SerializeField, Min(0)] private float _rechargeTime;
    [SerializeField, Min(0.1f)] private float _attackForSecond;
    [SerializeField, Min(0)] private float _timeBetweenDawnOut;

    private Coroutine _coroutine;
    private Health _characterHealth;
    private Health _targetHealth;
    private Timer _pullOutTimer;
    private Timer _rechargeTimer;
    private bool _isTargetFind = false;

    private void Awake()
    {
        _characterHealth = GetComponent<Health>();
        _pullOutTimer = new Timer(_timeWork);
        _rechargeTimer = new Timer(_rechargeTime);
    }

    private void Update()
    {
        FindTarget();

        if (Input.GetKeyDown(ActiveVampirism))
        {
            BegineDrawOutHealth();
        }

        _pullOutTimer.MakeCountdown();
        _rechargeTimer.MakeCountdown();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_pointZone.position, _radius);
    }

    private void FindTarget()
    {
        Collider2D target = Physics2D.OverlapCircle(_pointZone.position, _radius, _layer.value);

        if (target != null)
        {
            if (target.TryGetComponent(out Health health))
            {
                _targetHealth = health;
                _isTargetFind = true;
            }
        }
        else
        {
            _isTargetFind = false;

            LostTarget();
        }
    }

    private void LostTarget()
    {
        if (_coroutine != null && !_pullOutTimer.IsTimeUp)
        {
            StopCoroutine(_coroutine);

            _rechargeTimer.UpdateWaitingTime();
        }
    }

    private void BegineDrawOutHealth()
    {
        if (_isTargetFind && _pullOutTimer.IsTimeUp && _rechargeTimer.IsTimeUp)
        {
            _pullOutTimer.UpdateWaitingTime();

            _coroutine = StartCoroutine(DrawOutHealth());
        }
    }

    private IEnumerator DrawOutHealth()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeBetweenDawnOut);

        float oldHealth;
        float actualDamage;

        while (!_pullOutTimer.IsTimeUp)
        {
            oldHealth = _targetHealth.CurrentHealth;
            _targetHealth.TakeDamage(_attackForSecond);

            actualDamage = oldHealth - _targetHealth.CurrentHealth;

            _characterHealth.RestoreHealth(actualDamage);

            yield return wait;
        }

        _rechargeTimer.UpdateWaitingTime();
    }
}
