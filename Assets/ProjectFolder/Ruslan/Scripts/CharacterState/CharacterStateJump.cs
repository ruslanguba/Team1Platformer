using UnityEngine;

public class CharacterStateJump : CharacterStateBase
{
    public CharacterStateJump(CharacterStateMachine stateMachine, CharacterMovement movement, Rigidbody2D rb)
        : base(stateMachine, movement, rb) { }

    public override void Enter()
    {
        _movement.Jump();
    }

    public override void Update()
    {
        if (_rb.linearVelocity.y < 0)
        {
            _stateMachine.Fall();
        }
    }
}
