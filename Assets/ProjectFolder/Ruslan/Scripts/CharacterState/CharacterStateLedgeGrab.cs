using UnityEngine;

public class CharacterStateLedgeGrab : CharacterStateBase
{
    public CharacterStateLedgeGrab(CharacterStateMachine stateMachine, CharacterMovement movement, Rigidbody2D rb, CharacterLedgeHandler ledgeHandler)
        : base(stateMachine, movement, rb, ledgeHandler) { }

    public override void Enter()
    {
        _ledgeHandler.GrabLedge();
        Debug.Log("Персонаж висит на уступе");
    }

    public override void Move(Vector2 direction)
    {
        if (direction.y > 0)
        {
            _stateMachine.SetStateClimbLedge();
        }
        else if (direction.y < 0)
        {
            _ledgeHandler.ReleaseLedge();
            _stateMachine.SetStateFall();
        }
    }

    public override void Jump()
    {
        _stateMachine.SetStateClimbLedge();
    }
}
