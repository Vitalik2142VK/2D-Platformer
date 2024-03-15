using UnityEngine;

public class PlayerHealth : Health
{
    public override void Remove()
    {
        Debug.Log("You dead");
    }

    public void RestoreHealth(float health)
    {
        if (CurrentHealth + health > MaxCountHealth)
            CurrentHealth = MaxCountHealth;
        else
            CurrentHealth += health;

        RunEventHealthChange();
    }
}
