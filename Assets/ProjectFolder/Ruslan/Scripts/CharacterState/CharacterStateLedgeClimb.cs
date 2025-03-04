using UnityEngine;

public class CharacterStateLedgeClimb : CharacterStateBase
{
    private CharacterLedgeHandler _ledgeHandler;

    public CharacterStateLedgeClimb(CharacterStateMachine stateMachine, CharacterMovement movement, Rigidbody2D rb, CharacterLedgeHandler ledgeHandler)
        : base(stateMachine, movement, rb)
    {
        _ledgeHandler = ledgeHandler;
    }

    public override void Enter()
    {
        _ledgeHandler.ClimbLedge();
    }
}
