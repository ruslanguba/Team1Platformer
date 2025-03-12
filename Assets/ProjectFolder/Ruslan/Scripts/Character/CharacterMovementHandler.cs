using UnityEngine;

public class CharacterMovementHandler : MonoBehaviour
{
    private PlayerInputHandler _inputHandler;
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private float _airMoveSpeed = 5;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private bool _isFacingRight = true;
    private float _currentMoveDirection;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); ;
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            GroundMovement(_currentMoveDirection);
        }
        else
        {
            AirMovement(_currentMoveDirection);
        }
    }

    private void OnEnable()
    {
        _inputHandler.OnMoveInput += Move;
        _inputHandler.OnJumpInput += Jump;
    }

    private void OnDisable()
    {
        _inputHandler.OnMoveInput -= Move;
        _inputHandler.OnJumpInput -= Jump;
    }

    private void Move(Vector2 direction)
    {
        _currentMoveDirection = direction.x;
    }

    public void GroundMovement(float direction)
    {
        //_rb.linearVelocityX = direction * _moveSpeed;
        _rb.linearVelocity = new Vector2(direction * _moveSpeed, _rb.linearVelocityY);
        CheckDirection(direction);
    }

    public void AirMovement(float direction)
    {
        if (direction != 0)
        {
            // Получаем текущую горизонтальную скорость
            float currentSpeed = Mathf.Abs(_rb.linearVelocity.x);

            // Если текущая скорость меньше максимальной, добавляем силу
            if (currentSpeed < _airMoveSpeed)
            {
                // Рассчитываем силу в нужном направлении
                Vector2 force = new Vector2(direction * _airMoveSpeed, 0);

                // Применяем силу
                _rb.AddForce(force, ForceMode2D.Force);
            }
        }
        CheckDirection(direction);
    }

    public void Jump()
    {
        if(IsGrounded())
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }

    private void CheckDirection(float direction)
    {
        if ((direction > 0 && !_isFacingRight) || (direction < 0 && _isFacingRight))
        {
            FlipDirection();
        }
    }

    public void FlipDirection()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
