using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private Health _health;

    protected Health Health => _health;

    public void TakeDamage(float damageAmount)
    {
        _health.TakeDamage(damageAmount);
    }

    protected abstract void Die();
}
