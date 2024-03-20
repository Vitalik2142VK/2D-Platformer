using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(0)] private float _maxCountHealth;

    public event Action HealthOver;
    public event Action<float> HealthChange;

    public float MaxCountHealth => _maxCountHealth;
    public bool IsAlive { get; protected set; } = true;

    public float CurrentHealth { get; private set; }

    protected void Start()
    {
        CurrentHealth = _maxCountHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damageAmount, 0.0f, _maxCountHealth);

        if (CurrentHealth == 0)
        {
            IsAlive = false;

            HealthOver?.Invoke();
        }
            
        HealthChange?.Invoke(CurrentHealth);
    }

    public void RestoreHealth(float health)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + health, 0.0f, _maxCountHealth);

        HealthChange?.Invoke(CurrentHealth);
    }
}
