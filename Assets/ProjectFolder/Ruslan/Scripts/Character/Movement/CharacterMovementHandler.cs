using UnityEngine;

public class CharacterMovementHandler : MonoBehaviour, IMoveable
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _airAcceleration = 5;
    [SerializeField] private float _airMaxSpeed = 5;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    private Connector _connector;
    private bool _isFlipBlocked = false;
    private bool _isFacingRight = true;
    private bool _isGrounded;
    [SerializeField] private float _currentMoveDirection; // delate SerializeField
    private Rigidbody2D _rb;

    private void Awake()
    {
        _connector = GetComponent<Connector>();
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); 
    }

    private void OnEnable()
    {
        _connector.OnConnect += LockFlip;
        _connector.OnDisconnect += UnlockFlip;
    }

    private void OnDisable()
    {
        _connector.OnConnect -= LockFlip;
        _connector.OnDisconnect -= UnlockFlip;
    }


    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            GroundMovement();
        }
        else
        {
            AirMovement();
        }
    }

    public void Move(Vector2 direction)
    {
        _currentMoveDirection = direction.x;
        CheckDirection(direction.x);
    }

    public void GroundMovement()
    {
        _rb.linearVelocityX = _currentMoveDirection * _moveSpeed;
    }

    public void AirMovement()
    {
        if (_currentMoveDirection != 0)
        {
            // Получаем текущую горизонтальную скорость
            float currentSpeed = Mathf.Abs(_rb.linearVelocity.x);

            // Если текущая скорость меньше максимальной, добавляем силу
            if (currentSpeed < _airMaxSpeed)
            {
                // Рассчитываем силу в нужном направлении
                Vector2 force = new Vector2(_currentMoveDirection * _airAcceleration, 0);

                // Применяем силу
                _rb.AddForce(force, ForceMode2D.Force);
            }
        }
    }

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
        if (!_isFlipBlocked)
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    public int GetFacingDirection()
    {
        if (_isFacingRight)
            return 1;
        else return -1;
    }

    public void LockFlip(Transform _)
    {
        _isFlipBlocked = true;   
    }

    public void UnlockFlip(Transform connectedBody)
    {
        _isFlipBlocked = false;

        bool isMovingBackwards =
        (_isFacingRight &&
        ((transform.position.x > connectedBody.position.x && _currentMoveDirection > 0) ||
         (transform.position.x < connectedBody.position.x && _currentMoveDirection < 0)))
        ||
        (!_isFacingRight &&
        ((transform.position.x > connectedBody.position.x && _currentMoveDirection > 0) ||
         (transform.position.x < connectedBody.position.x && _currentMoveDirection < 0)));
        if (isMovingBackwards)
        {
            FlipDirection();
        }
    }


}
