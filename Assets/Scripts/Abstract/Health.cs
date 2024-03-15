using System;
using UnityEngine;

public abstract class Health : MonoBehaviour
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

        RunEventHealthChange();
    }

    protected void EstablishMaxHealth()
    {
        CurrentHealth = _maxCountHealth;
    }

    protected void RunEventHealthChange()
    {
        HealthChange?.Invoke(CurrentHealth);
    }

    public abstract void Remove();
}
