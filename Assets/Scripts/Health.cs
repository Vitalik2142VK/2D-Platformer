using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Min(0)] private float _maxCountHealth;

    public event Action HealthOver;
    public event Action<float> HealthChange;

    public float MaxCountHealth => _maxCountHealth;
    public bool IsAlive { get; protected set; } = true;

    protected float CurrentHealth { get; set; }

    protected void Start()
    {
        EstablishMaxHealth();
    }

    public void TakeDamage(float damageAmount)
    {
        if (CurrentHealth <= damageAmount)
        {
            CurrentHealth = 0;
            IsAlive = false;

            HealthOver?.Invoke();
        }
        else
        {
            CurrentHealth -= damageAmount;
        }

        HealthChange?.Invoke(CurrentHealth);
    }

    public void RestoreHealth(float health)
    {
        if (CurrentHealth + health > MaxCountHealth)
            CurrentHealth = MaxCountHealth;
        else
            CurrentHealth += health;

        HealthChange?.Invoke(CurrentHealth);
    }

    protected void EstablishMaxHealth()
    {
        CurrentHealth = _maxCountHealth;
    }
}
