using UnityEngine;

public class EnemyMeleeAttack : MeleeAttack
{
    protected override void AttackTargets()
    {
        Collider2D[] enemies = GetEnemies();

        if (enemies.Length > 0)
        {
            Animator.SetTrigger(HashIsMeleeAttack);

            foreach (var enemy in enemies)
            {
                enemy.GetComponent<Health>().TakeDamage(Damage);
            }

            UpdateTimer();
        }
    }
}
