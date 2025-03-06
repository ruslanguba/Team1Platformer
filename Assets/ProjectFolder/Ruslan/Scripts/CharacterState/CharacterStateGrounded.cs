using UnityEngine;

public class CharacterStateGrounded : CharacterStateBase
{
    public CharacterStateGrounded(CharacterStateMachine stateMachine, CharacterMovement movement, Rigidbody2D rb, CharacterLedgeHandler ledgeHandler)
        : base(stateMachine, movement, rb, ledgeHandler) { }

    public override void Enter()
    {
        _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
    }

    public override void Update()
    {
        if (!_movement.IsGrounded())
        {
            _stateMachine.SetStateFall();
        }
    }

    public override void Move(Vector2 direction)
    {
        _movement.GroundMovement(direction.x);
    }

    public override void Jump()
    {
        if (_ledgeHandler.IsGrabbingLedge())
        {
            _stateMachine.SetStateClimbLedge();
        }
        else
        {
            _movement.Jump();
            _stateMachine.SetStateJump();
        }
    }
}
