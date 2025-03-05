using UnityEngine;

public class CharacterStateGrounded : CharacterStateBase
{
    public CharacterStateGrounded(CharacterStateMachine stateMachine, CharacterMovement movement, Rigidbody2D rb, CharacterLedgeHandler ledgeHandler)
        : base(stateMachine, movement, rb, ledgeHandler) { }

    public override void Enter()
    {
        Debug.Log("Персонаж стоит на земле");
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
        _movement.Jump();
        _stateMachine.SetStateJump();
    }
}
