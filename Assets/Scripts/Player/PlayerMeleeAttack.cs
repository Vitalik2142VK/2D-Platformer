using UnityEngine;

public class PlayerMeleeAttack : MeleeAttack
{
    protected override void AttackTargets()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Animator.SetTrigger(HashIsMeleeAttack);

            Collider2D[] enemies = GetEnemies();

            if (enemies.Length > 0)
            {
                foreach (var enemy in enemies)
                {
                    enemy.GetComponent<Health>().TakeDamage(Damage);
                }
            }

            UpdateTimer();
        }
    }
}
