using UnityEngine;

public class CharacterStateMachine : MonoBehaviour 
{
    public CharacterStateBase CurrentState { get; private set; }

    private CharacterStateGrounded _groundedState;
    private CharacterStateJump _jumpingState;
    private CharacterStateInAir _inAirState;
    private CharacterStateLedgeGrab _ledgeGrabState;
    private CharacterStateLedgeClimb _ledgeClimbState;
    private CharacterStateWallSlide _wallSlideState;
    private CharacterStateWater _waterState;
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

        InintStates();
    }

    private void OnEnable()
    {
        _inputHandler.OnMoveInput += Move;
        _inputHandler.OnJumpInput += OnJump;
        //_ledgeHandler.OnLedgeGrabbed += SetStateGrabLedge;
        _ledgeHandler.OnClimbFinished += SetStateLand;
    }

    private void OnDisable()
    {
        _inputHandler.OnMoveInput -= Move;
        _inputHandler.OnJumpInput -= OnJump;
        //_ledgeHandler.OnLedgeGrabbed -= SetStateGrabLedge;
        _ledgeHandler.OnClimbFinished -= SetStateLand;
    }

    private void FixedUpdate()
    {
        CurrentState.Update();
    }

    private void InintStates()
    {
        _groundedState = new CharacterStateGrounded(this, _movement, _rb, _ledgeHandler);
        _jumpingState = new CharacterStateJump(this, _movement, _rb, _ledgeHandler);
        _inAirState = new CharacterStateInAir(this, _movement, _rb, _ledgeHandler);
        _ledgeGrabState = new CharacterStateLedgeGrab(this, _movement, _rb, _ledgeHandler);
        _ledgeClimbState = new CharacterStateLedgeClimb(this, _movement, _rb, _ledgeHandler);
        _wallSlideState = new CharacterStateWallSlide(this, _movement, _rb, _ledgeHandler);
        _waterState = new CharacterStateWater(this, _movement, _rb, _ledgeHandler);
        CurrentState = _groundedState; // Начальное состояние
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

    public void OnJump()
    {
        CurrentState.Jump();
    }

    public void SetStateJump()
    {
        SetState(_jumpingState);
    }

    public void SetStateFall()
    {
        SetState(_inAirState);
    }

    public void SetStateLand()
    {
        SetState(_groundedState);
    }

    public void SetStateGrabLedge()
    {
        SetState(_ledgeGrabState);
    }

    public void SetStateClimbLedge()
    {
        SetState(_ledgeClimbState);
    }

    public void SetStateWallSlide()
    {
        SetState(_wallSlideState);
    }
}
