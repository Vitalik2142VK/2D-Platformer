using UnityEngine;

public class PlayerMover : Mover
{
    private const string Horizontal = nameof(Horizontal);
    private const string IsRunning = nameof(IsRunning);
    private const string IsJumping = nameof(IsJumping);

    [SerializeField, Min(0)] private float _forceJump;

    private int _hashIsRunning = Animator.StringToHash(IsRunning);
    private int _hashIsJumping = Animator.StringToHash(IsJumping);

    public void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsInAir == false)
        {
            IsInAir = true;
            Rigidbody.velocity = _forceJump * Vector2.up;
        }

        Animator.SetBool(_hashIsJumping, IsInAir && Rigidbody.velocity.y > 0);
    }

    public override void Move()
    {
        float directionX = Input.GetAxis(Horizontal);

        Flip(directionX);

        Animator.SetBool(_hashIsRunning, directionX != 0 && IsInAir == false);

        transform.Translate(directionX * Speed * Time.deltaTime * Vector2.right, Space.World);
    }
}
