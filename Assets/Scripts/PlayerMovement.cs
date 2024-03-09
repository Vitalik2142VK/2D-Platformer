using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string IsRunning = nameof(IsRunning);
    private const int RotateByY = 180;

    [SerializeField] private Animator _animator;
    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private float _forceJump;

    private Rigidbody2D _playerRigidbody;
    private int _hashIsRunning = Animator.StringToHash(IsRunning);
    private bool _isDerictionRight = true;
    private bool _isInAir = false;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Jump();
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isInAir = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _isInAir = true;
    }

    private void Move()
    {
        float horizontal = Input.GetAxis(Horizontal);

        if (_isDerictionRight && horizontal < 0)
        {
            Flip();
        }
        else if (!_isDerictionRight && horizontal > 0)
        {
            Flip();
        }

        _animator.SetBool(_hashIsRunning, horizontal != 0);

        transform.Translate(horizontal * _speed * Time.deltaTime * Vector2.right, Space.World);
    }

    private void Flip()
    {
        _isDerictionRight = !_isDerictionRight;

        transform.Rotate(0, RotateByY, 0);
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _isInAir == false)
        {
            _isInAir = true;
            _playerRigidbody.velocity = _forceJump * Vector2.up;
        }
    }
}
