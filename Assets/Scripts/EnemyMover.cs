using System.Collections;
using UnityEngine;

public class EnemyMover : Mover
{
    private const string IsWalking = nameof(IsWalking);
    private const float MaxWaitingTimeAtPoint = 5;
    private const float MinWaitingTimeAtPoint = 1;
    private const int DefaultIndexPoint = -1;

    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _radiusReachingPoint;
    [SerializeField] private bool _routeLooped = true;

    private int _hashIsWalking = Animator.StringToHash(IsWalking);
    private int _currentIndexPoint = DefaultIndexPoint;
    private bool _isPointReached = false;

    private new void Start()
    {
        if (_waypoints.Length != 0)
        {
            _currentIndexPoint = 0;
        }

        base.Start();
    }

    public override void Move()
    {
        if (_currentIndexPoint != DefaultIndexPoint)
        {
            Vector2 positionWaypoint = _waypoints[_currentIndexPoint].position;
            Vector2 previousPosition = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, positionWaypoint, Speed * Time.deltaTime);

            float directionX = transform.position.x - previousPosition.x;

            Flip(directionX);

            Animator.SetBool(_hashIsWalking, directionX != 0 && IsInAir == false);

            float distance = Vector2.Distance(transform.position, positionWaypoint);

            if (distance < _radiusReachingPoint && _isPointReached == false)
            {
                _isPointReached = true;

                StartCoroutine(SpecifyNextPoint());
            }
        }
    }

    private IEnumerator SpecifyNextPoint()
    {
        yield return new WaitForSeconds(Random.Range(MinWaitingTimeAtPoint, MaxWaitingTimeAtPoint));

        _currentIndexPoint++;

        if (_currentIndexPoint == _waypoints.Length)
        {
            if (_routeLooped)
                _currentIndexPoint = 0;
            else
                _currentIndexPoint = DefaultIndexPoint;
        }

        _isPointReached = false;
    }
}
