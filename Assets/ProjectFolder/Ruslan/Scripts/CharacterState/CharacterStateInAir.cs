using UnityEngine;

public class CharacterStateInAir : CharacterStateBase
{
    public CharacterStateInAir(CharacterStateMachine stateMachine, CharacterMovement movement, Rigidbody2D rb)
        : base(stateMachine, movement, rb) { }

    public override void Enter()
    {
        Debug.Log("Персонаж в воздухе");
    }

    public override void Update()
    {
        if (_movement.IsGrounded())
        {
            _stateMachine.Land();
        }
    }

    public override void Move(Vector2 direction)
    {
        _movement.AirMovement(direction.x); // В воздухе замедленное движение
    }
}
