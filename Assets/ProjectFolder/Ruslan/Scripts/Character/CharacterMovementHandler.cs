using UnityEngine;

public class CharacterMovementHandler : MonoBehaviour
{
    private PlayerInputHandler _inputHandler;
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private float _airAcceleration = 5;
    [SerializeField] private float _airMaxSpeed = 5;
    [SerializeField] private float _gravity = -9.8f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private bool _isFacingRight = true;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private bool _isJumpVelocity;
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
        //_inputHandler.OnJumpInput += Jump;
    }

    private void OnDisable()
    {
        _inputHandler.OnMoveInput -= Move;
        //_inputHandler.OnJumpInput -= Jump;
    }

    private void Move(Vector2 direction)
    {
        _currentMoveDirection = direction.x;
        CheckDirection(direction.x);
    }

    public void GroundMovement(float direction)
    {
        _rb.linearVelocityX = direction * _moveSpeed;
        //_rb.linearVelocity = new Vector2(direction * _moveSpeed, _rb.linearVelocityY);
    }

    public void AirMovement(float direction)
    {
        if (direction != 0)
        {
            // Получаем текущую горизонтальную скорость
            float currentSpeed = Mathf.Abs(_rb.linearVelocity.x);

            // Если текущая скорость меньше максимальной, добавляем силу
            if (currentSpeed < _airMaxSpeed)
            {
                // Рассчитываем силу в нужном направлении
                Vector2 force = new Vector2(direction * _airAcceleration, 0);

                // Применяем силу
                _rb.AddForce(force, ForceMode2D.Force);
            }
        }
    }

    //public void Jump()
    //{
    //    if(IsGrounded())
    //    {
    //        if(_isJumpVelocity)
    //        {
    //            _rb.linearVelocityY = _jumpForce;
    //            Debug.Log("Velocity");
    //        }
    //        else
    //        {
    //            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    //            Debug.Log("AddForceJump");
    //        }
    //    }
    //}

    public bool IsGrounded()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        return _isGrounded;
    }

    private void CheckDirection(float direction)
    {
        if ((direction > 0 && !_isFacingRight) || (direction < 0 && _isFacingRight))
        {
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            FlipDirection();
        }
    }

    public void FlipDirection()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public int GetFacingDirection()
    {
        if (_isFacingRight)
            return 1;
        else return -1;
    }
}
