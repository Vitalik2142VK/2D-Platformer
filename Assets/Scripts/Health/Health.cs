using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(0)] private float _maxCountHealth;

    public event Action Over;
    public event Action<float> Change;

    public float MaxCountHealth => _maxCountHealth;
    public bool IsAlive => CurrentHealth > 0;

    public float CurrentHealth { get; private set; }

    protected void Start()
    {
        CurrentHealth = _maxCountHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        float currentHealth = CurrentHealth - damageAmount;

        CurrentHealth = Mathf.Clamp(currentHealth, 0.0f, _maxCountHealth);

        if (IsAlive == false)
        {
            Over?.Invoke();
        }
            
        Change?.Invoke(CurrentHealth);
    }

    public void RestoreHealth(float health)
    {
        float currentHealth = CurrentHealth + health;

        CurrentHealth = Mathf.Clamp(currentHealth, 0.0f, _maxCountHealth);

        Change?.Invoke(CurrentHealth);
    }
}
