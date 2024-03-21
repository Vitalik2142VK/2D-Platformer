using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public abstract class Mover : MonoBehaviour
{
    private const string IsFalling = nameof(IsFalling);
    private const int RotateY = 180;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Animator _animator;
    [SerializeField, Min(0)] private float _speed;
    [SerializeField] private float _activationRadiusGround;
    [SerializeField] private bool _isRadiusAreaEnabled;

    private int _hashIsFalling = Animator.StringToHash(IsFalling);
    private bool _isDerictionRight = true;

    protected Rigidbody2D Rigidbody;

    public bool IsGrounded { get; private set; }

    protected Animator Animator => _animator;
    protected float Speed => _speed;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnDrawGizmosSelected()
    {
        if (_isRadiusAreaEnabled)
        {
             Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _activationRadiusGround);
        }
    }

    private void FixedUpdate()
    {
        IsGrounded = IsOnGround();
    }

    public void Fall()
    {
        _animator.SetBool(_hashIsFalling, IsGrounded == false && Rigidbody.velocity.y < 0);
    }

    protected void Flip(float directionX)
    {
        if (directionX == 0)
            return;

        if (_isDerictionRight == true && directionX < 0)
            RotateByY();
        else if (_isDerictionRight == false && directionX > 0)
            RotateByY();
    }

    private void RotateByY()
    {
        _isDerictionRight = !_isDerictionRight;

        transform.Rotate(0, RotateY, 0);
    }

    private bool IsOnGround()
    {
        return Physics2D.OverlapCircle(transform.position, _activationRadiusGround, _layerMask.value);
    }

    public abstract void Move();
}
