using UnityEngine;

public class CharacterStateBase
{
    protected CharacterStateMachine _stateMachine;
    protected CharacterMovement _movement;
    protected CharacterLedgeHandler _ledgeHandler;
    protected Rigidbody2D _rb;

    protected CharacterStateBase(CharacterStateMachine stateMachine, CharacterMovement movement, Rigidbody2D rb, CharacterLedgeHandler ledgeHandler)
    {
        _ledgeHandler = ledgeHandler;
        _stateMachine = stateMachine;
        _movement = movement;
        _rb = rb;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void Move(Vector2 direction) { }
    public virtual void Jump() { }
}
