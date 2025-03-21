using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    private Rigidbody2D _rb;
    private PlayerInputHandler _inputHandler;
    private CharacterMovementHandler _movementHandler; // Добавляем ссылку на CharacterMovementHandler

    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;
    private float _gravityAbs;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _movementHandler = GetComponent<CharacterMovementHandler>(); // Получаем ссылку на CharacterMovementHandler
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    private void OnEnable()
    {
        _inputHandler.OnJumpInput += Jump;
    }

    private void OnDisable()
    {
        _inputHandler.OnJumpInput -= Jump;
    }

    private void Start()
    {
        _gravityAbs = Mathf.Abs(Physics2D.gravity.y);
    }

    public void Jump()
    {
        if (_movementHandler.IsGrounded()) // Используем метод из CharacterMovementHandler
        {
            _rb.linearVelocityY = _jumpForce;
        }
    }

    private void FixedUpdate()
    {
        if(!_movementHandler.IsGrounded())
        {
            ApplyCustomGravity();
        }
    }

    private void ApplyCustomGravity()
    {
        if (_rb.linearVelocityY < 0)
        {
            _rb.AddForce(Vector2.down * (_fallMultiplier - 1) * _gravityAbs, ForceMode2D.Force);
        }
        else if (_rb.linearVelocityY > 0)
        {
            _rb.AddForce(Vector2.down * (_lowJumpMultiplier - 1) * _gravityAbs, ForceMode2D.Force);
        }
    }
}
