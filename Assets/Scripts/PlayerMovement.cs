using UnityEngine;

public class PlayerMovement : Movement
{
    private const string Horizontal = nameof(Horizontal);
    private const string IsRunning = nameof(IsRunning);
    private const string IsJumping = nameof(IsJumping);

    [SerializeField, Min(0)] private float _forceJump;

    private int _hashIsRunning = Animator.StringToHash(IsRunning);
    private int _hashIsJumping = Animator.StringToHash(IsJumping);

    private new void Update()
    {
        Jump();
        base.Update();
    }

    private void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && IsInAir == false)
        {
            IsInAir = true;
            Rigidbody.velocity = _forceJump * Vector2.up;
        }

        Animator.SetBool(_hashIsJumping, IsInAir && Rigidbody.velocity.y > 0);
    }

    protected override void Move()
    {
        float directionByX = Input.GetAxis(Horizontal);

        Flip(directionByX);

        Animator.SetBool(_hashIsRunning, directionByX != 0 && IsInAir == false);

        transform.Translate(directionByX * Speed * Time.deltaTime * Vector2.right, Space.World);
    }
}
