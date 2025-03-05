using UnityEngine;

public class CharacterStateLedgeClimb : CharacterStateBase
{
    public CharacterStateLedgeClimb(CharacterStateMachine stateMachine, CharacterMovement movement, Rigidbody2D rb, CharacterLedgeHandler ledgeHandler)
        : base(stateMachine, movement, rb, ledgeHandler)
    {
        _ledgeHandler = ledgeHandler;
    }

    public override void Enter()
    {
        _ledgeHandler.ClimbLedge();
    }
}
