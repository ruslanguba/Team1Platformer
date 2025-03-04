using UnityEngine;

public class CharacterStateMachine : MonoBehaviour 
{
    public CharacterStateBase CurrentState { get; private set; }

    private CharacterStateGrounded _groundedState;
    private CharacterStateJump _jumpingState;
    private CharacterStateInAir _inAirState;
    private CharacterStateLedgeGrab _ledgeGrabState;
    private CharacterStateLedgeClimb _ledgeClimbState;

    private PlayerInputHandler _inputHandler;
    private CharacterMovement _movement;
    private CharacterLedgeHandler _ledgeHandler;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _movement = GetComponent<CharacterMovement>();
        _inputHandler = GetComponent<PlayerInputHandler>();
        _ledgeHandler = GetComponent<CharacterLedgeHandler>();
        _rb = GetComponent<Rigidbody2D>();
        _groundedState = new CharacterStateGrounded(this, _movement, _rb);
        _jumpingState = new CharacterStateJump(this, _movement, _rb);
        _inAirState = new CharacterStateInAir(this, _movement, _rb);
        _ledgeGrabState = new CharacterStateLedgeGrab(this, _movement, _rb, _ledgeHandler);
        _ledgeClimbState = new CharacterStateLedgeClimb(this, _movement, _rb, _ledgeHandler);

        CurrentState = _groundedState; // Начальное состояние
    }

    private void OnEnable()
    {
        _inputHandler.OnMoveInput += Move;
        _inputHandler.OnJumpInput += Jump;
        _ledgeHandler.OnLedgeGrabbed += GrabLedge;
        _ledgeHandler.OnClimbFinished += Land;
    }

    private void OnDisable()
    {
        _inputHandler.OnMoveInput -= Move;
        _inputHandler.OnJumpInput -= Jump;
        _ledgeHandler.OnLedgeGrabbed -= GrabLedge;
        _ledgeHandler.OnClimbFinished -= Land;
    }

    private void FixedUpdate()
    {
        CurrentState.Update();
    }

    public void SetState(CharacterStateBase newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

    public void Move(Vector2 direction)
    {
        CurrentState.Move(direction);
    }

    public void Jump()
    {
        if (CurrentState == _groundedState)
            SetState(_jumpingState);
    }

    public void Fall()
    {
        SetState(_inAirState);
    }

    public void Land()
    {
        SetState(_groundedState);
    }

    public void GrabLedge()
    {
        SetState(_ledgeGrabState);
    }

    public void ClimbLedge()
    {
        SetState(_ledgeClimbState);
    }
}
