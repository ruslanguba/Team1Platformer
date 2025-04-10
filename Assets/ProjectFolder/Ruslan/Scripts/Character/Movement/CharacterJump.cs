using System;
using UnityEngine;

public class CharacterJump : MonoBehaviour, IJumpable
{
    public Action OnJump;
    private Rigidbody2D _rb;
    private CharacterMovementHandler _movementHandler; // Добавляем ссылку на CharacterMovementHandler
    [SerializeField] private float _maxHoldTime = 0.5f;
    [SerializeField] private float _minHoldTime = 0.15f;
    [SerializeField] private float _shortJumpMaxTime = 4;
    [SerializeField] private float _shortJumpHeight = 3;
    private float _shortJumpStartVelocity;

    [SerializeField] private float _longJumpExtraForce = 8;

    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;
    [SerializeField] private bool _isVelocityMode = false;

    private bool _isJumpHeld = false;
    private bool _isJumping = false;
    private float _holdTimer = 0f;
    private float _gravityAbs;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _movementHandler = GetComponent<CharacterMovementHandler>(); // Получаем ссылку на CharacterMovementHandler
    }

    private void Start()
    {
        _gravityAbs = Mathf.Abs(Physics2D.gravity.y);
        float shortJumpHeightTime = _shortJumpMaxTime / 2;
        _shortJumpStartVelocity = (2 * _shortJumpHeight) / shortJumpHeightTime;
    }

    public void Jump()
    {
        if (_movementHandler.IsGrounded())
        {
            _rb.linearVelocityY = _shortJumpStartVelocity;
            _isJumping = true;
            _holdTimer = 0f;
            OnJump?.Invoke();
            Debug.Log("Short jump");
        }
    }

    public void SetJumpHeld(bool isHeld)
    {
        _isJumpHeld = isHeld;

        // Если кнопку отпустили — прекращаем удлинять прыжок
        if (!isHeld)
        {
            _isJumping = false;
        }
    }

    void PerformJump()
    {
        if (_isJumping && _isJumpHeld)
        {
            _holdTimer += Time.fixedDeltaTime;

            if (_holdTimer <= _maxHoldTime )
            {
                if(_holdTimer >= _minHoldTime)
                {
                    _rb.linearVelocityY += _longJumpExtraForce * Time.fixedDeltaTime;
                    Debug.Log("Extending jump");
                }
            }
            else
            {
                _isJumping = false; // достигли максимального времени — прекращаем
            }
        }
    }

    private void FixedUpdate()
    {
        PerformJump();
        if (!_movementHandler.IsGrounded())
        {
            ApplyCustomGravity();
        }
    }

    private void ApplyCustomGravity()
    {
        if(_isVelocityMode)
        {
            if (_rb.linearVelocityY < 0)
            {
                _rb.linearVelocityY -= _gravityAbs * (_fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (_rb.linearVelocityY > 0)
            {
                _rb.linearVelocityY -= _gravityAbs * (_lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
        }
        else
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
}
