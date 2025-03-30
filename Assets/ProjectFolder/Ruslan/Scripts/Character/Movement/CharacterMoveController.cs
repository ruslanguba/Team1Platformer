using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler _input;
    [SerializeField] private IMoveable _moveable;
    [SerializeField] private IJumpable _jumpable;

    private void Awake()
    {
        _input = GetComponent<PlayerInputHandler>();
        _moveable = GetComponent<IMoveable>();
        _jumpable = GetComponent<IJumpable>();
    }

    private void OnEnable()
    {
        if(_moveable != null)
            _input.OnMoveInput += _moveable.Move;
        if(_jumpable != null) 
            _input.OnJumpInput += _jumpable.Jump;
    }

    private void OnDisable()
    {
        if (_moveable != null)
            _input.OnMoveInput -= _moveable.Move;
        if (_jumpable != null)
            _input.OnJumpInput -= _jumpable.Jump;
    }

    public void StopMovement()
    {
        _moveable.Move(Vector2.zero);
    }
}
