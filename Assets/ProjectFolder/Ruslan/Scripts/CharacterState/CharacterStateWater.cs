using UnityEngine;

public class CharacterStateWater : CharacterStateBase
{
    private float _speedModifier;
    public CharacterStateWater(CharacterStateMachine stateMachine, CharacterMovement movement, Rigidbody2D rb, CharacterLedgeHandler ledgeHandler)
      : base(stateMachine, movement, rb, ledgeHandler) { }

    public override void Enter()
    {
        _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
        _speedModifier = 0.7f;
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
        _movement.GroundMovement(direction.x * _speedModifier);
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
