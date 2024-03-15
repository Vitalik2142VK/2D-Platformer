using System.Collections;
using UnityEngine;

public class EnemyMover : Mover
{
    private const string IsWalking = nameof(IsWalking);
    private const float MaxWaitingTimeAtPoint = 5;
    private const float MinWaitingTimeAtPoint = 1;
    private const int DefaultIndexPoint = -1;

    [SerializeField] private EnemyFindPlayer _playerSearchArea;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _radiusReachingPoint;
    [SerializeField] private bool _routeLooped = true;

    private Player _player;
    private int _hashIsWalking = Animator.StringToHash(IsWalking);
    private int _currentIndexPoint = DefaultIndexPoint;
    private bool _isPointReached = false;
    private bool _isPlayerFound = false;

    private void OnEnable()
    {
        _playerSearchArea.PlayerFound += ChangeMoveToPlayer;
    }

    private new void Start()
    {
        if (_waypoints.Length != 0)
        {
            _currentIndexPoint = 0;
        }

        base.Start();
    }

    private void OnDisable()
    {
        _playerSearchArea.PlayerFound -= ChangeMoveToPlayer;
    }

    public override void Move()
    {
        if (_currentIndexPoint != DefaultIndexPoint && _isPlayerFound == false)
        {
            MoveToPoints();
        }
        else if (_isPlayerFound)
        {
            MoveToPlayer();
        }
    }

    private void MoveToPoints()
    {
        Vector2 positionWaypoint = _waypoints[_currentIndexPoint].position;
        Vector2 previousPosition = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, positionWaypoint, Speed * Time.deltaTime);

        SpecifyAnimationDirection(previousPosition.x);

        float distance = Vector2.Distance(transform.position, positionWaypoint);

        if (distance < _radiusReachingPoint && _isPointReached == false)
        {
            _isPointReached = true;

            StartCoroutine(SpecifyNextPoint());
        }
    }

    private void MoveToPlayer()
    {
        Vector2 previousPosition = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, Speed * Time.deltaTime);

        SpecifyAnimationDirection(previousPosition.x);
    }

    private void SpecifyAnimationDirection(float previousPositionX)
    {
        float directionX = transform.position.x - previousPositionX;

        Flip(directionX);

        Animator.SetBool(_hashIsWalking, directionX != 0 && IsInAir == false);
    }

    private void ChangeMoveToPlayer(Player player, bool isPlayingFound)
    {
        _isPlayerFound = isPlayingFound;
        _player = player;
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
