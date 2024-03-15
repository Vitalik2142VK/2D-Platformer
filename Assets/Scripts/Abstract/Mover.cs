using UnityEngine;

public abstract class Mover : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherRigidbody == Rigidbody && collision.gameObject.TryGetComponent(out Land _))
        {
            IsInAir = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherRigidbody == Rigidbody && collision.gameObject.TryGetComponent(out Land _))
        {
            IsInAir = true;
        }
    }

    public void Fall()
    {
        _animator.SetBool(_hashIsFalling, IsInAir && Rigidbody.velocity.y < 0);
    }

    protected void Flip(float directionX)
    {
        if (directionX == 0)
            return;

        if (_isDerictionRight == true && directionX < 0)
        {
            _isDerictionRight = false;

            transform.Rotate(0, RotateByY, 0);
        } 
        else if (_isDerictionRight == false && directionX > 0)
        {
            _isDerictionRight = true;

            transform.Rotate(0, RotateByY, 0);
        }
    }

    public abstract void Move();
}
