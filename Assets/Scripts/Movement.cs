using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    private const string IsFalling = nameof(IsFalling);
    private const int RotateByY = 180;

    [SerializeField] private Animator _animator;
    [SerializeField, Min(0)] private float _speed;

    private int _hashIsFalling = Animator.StringToHash(IsFalling);
    private bool _isDerictionRight = true;

    protected Rigidbody2D Rigidbody;
    protected bool IsInAir = false;

    protected Animator Animator => _animator;
    protected float Speed => _speed;

    protected void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        Fall();
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherRigidbody == Rigidbody && collision.gameObject.TryGetComponent(out Land land))
        {
            IsInAir = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherRigidbody == Rigidbody && collision.gameObject.TryGetComponent(out Land land))
        {
            IsInAir = true;
        }
    }

    private void Fall()
    {
        _animator.SetBool(_hashIsFalling, IsInAir && Rigidbody.velocity.y < 0);
    }

    protected void Flip(float directionByX)
    {
        if (directionByX == 0)
            return;

        if (_isDerictionRight == true && directionByX < 0)
        {
            _isDerictionRight = false;

            transform.Rotate(0, RotateByY, 0);
        } 
        else if (_isDerictionRight == false && directionByX > 0)
        {
            _isDerictionRight = true;

            transform.Rotate(0, RotateByY, 0);
        }
    }

    protected abstract void Move();
}
