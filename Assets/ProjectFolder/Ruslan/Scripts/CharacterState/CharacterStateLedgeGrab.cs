using UnityEngine;

public class CharacterStateLedgeGrab : CharacterStateBase
{
    private CharacterLedgeHandler _ledgeHandler;

    public CharacterStateLedgeGrab(CharacterStateMachine stateMachine, CharacterMovement movement, Rigidbody2D rb, CharacterLedgeHandler ledgeHandler)
        : base(stateMachine, movement, rb)
    {
        _ledgeHandler = ledgeHandler;
    }

    public override void Enter()
    {
        _ledgeHandler.GrabLedge();
    }

    public override void Move(Vector2 direction)
    {
        if (direction.y > 0)
        {
            _stateMachine.ClimbLedge();
        }
        else if (direction.y < 0)
        {
            _ledgeHandler.ReleaseLedge();
            _ledgeHandler.DisableLedgeDetection();
            _stateMachine.Fall();
        }
    }
}
