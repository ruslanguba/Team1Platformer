using UnityEngine;

public class CharacterStateWallSlide : CharacterStateBase
{

    public CharacterStateWallSlide(CharacterStateMachine stateMachine, CharacterMovement movement, Rigidbody2D rb, CharacterLedgeHandler ledgeHandler)
        : base(stateMachine, movement, rb, ledgeHandler) { }

    public override void Enter()
    {
        Debug.Log("Персонаж скользит по стене");
        _rb.gravityScale = 0;
        _movement.WallSliding();
    }

    public override void Update()
    {
        if (_rb.linearVelocity.y == 0)
        {
            _movement.WallSliding();
        }

        if(_ledgeHandler.IsGrabbingLedge())
        {
            _stateMachine.SetStateGrabLedge();
            return;
        }

        if (_movement.IsGrounded())
        {
            _stateMachine.SetStateLand();
            return;
        }

        if (!_movement.IsWallSliding())
        {
            _stateMachine.SetStateFall();
            return;
        }
    }

    public override void Move(Vector2 direction)
    {
        if(direction.y < 0)
        {
            _rb.gravityScale = 1;
        }
    }

    public override void Jump()
    {
        _movement.WallJump();
        _stateMachine.SetStateJump();
    }

    public override void Exit()
    {
        _rb.gravityScale = 1;
    }
}
