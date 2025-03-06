using UnityEngine;

public class CharacterStateInAir : CharacterStateBase
{
    private int _jumpsLeft;
    public CharacterStateInAir(CharacterStateMachine stateMachine, CharacterMovement movement, Rigidbody2D rb, CharacterLedgeHandler ledgeHandler)
        : base(stateMachine, movement, rb, ledgeHandler) { }

    public override void Enter()
    {
        Debug.Log("Enter State In Air");
        _jumpsLeft = 1;
    }

    public override void Update()
    {
        if (_ledgeHandler.IsGrabbingLedge())
        {
            _stateMachine.SetStateGrabLedge();
            return;
        }

        if (_movement.IsGrounded() && IsFalling())
        {
            _stateMachine.SetStateLand();
            return;
        }

        if (!_movement.IsGrounded() && _movement.IsWallSliding() && IsFalling())
        {
            _stateMachine.SetStateWallSlide();
            return;
        }
    }

    private bool IsFalling()
    {
        return _rb.linearVelocity.y < 0;  // Если вертикальная скорость отрицательна, значит персонаж падает.
    }
    public override void Move(Vector2 direction)
    {
        _movement.AirMovement(direction.x);
        if (!_movement.IsGrounded() && _movement.IsWallSliding())
        {
            _stateMachine.SetStateWallSlide();
            return;
        }
    }
    public override void Jump()
    {
        if(_jumpsLeft > 0)
        {
            _jumpsLeft--;
            _movement.DoubleJump();
        }
    }
    public override void Exit()
    {
    }
}

