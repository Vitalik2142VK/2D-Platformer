using UnityEngine;

public class Enemy : MonoBehaviour 
{
    [SerializeField] private EnemyMover _enemyMover;

    private void Update()
    {
        _enemyMover.Fall();
        _enemyMover.Move();
    }
}
