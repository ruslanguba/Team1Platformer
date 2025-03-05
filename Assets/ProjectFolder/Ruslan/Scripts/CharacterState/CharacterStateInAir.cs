using UnityEngine;

public class CharacterStateInAir : CharacterStateBase
{
    public CharacterStateInAir(CharacterStateMachine stateMachine, CharacterMovement movement, Rigidbody2D rb, CharacterLedgeHandler ledgeHandler)
        : base(stateMachine, movement, rb, ledgeHandler) { }

    public override void Enter()
    {
        Debug.Log("Enter StateInAir");
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
    }

    public override void Exit()
    {
    }
}

