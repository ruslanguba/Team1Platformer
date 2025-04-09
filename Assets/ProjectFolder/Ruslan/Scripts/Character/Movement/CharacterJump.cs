using System;
using System.Collections;
using UnityEngine;

public class CharacterJump : MonoBehaviour, IJumpable
{
    public Action OnJump;
    private Rigidbody2D _rb;
    private CharacterMovementHandler _movementHandler; // Добавляем ссылку на CharacterMovementHandler
    [SerializeField] private float _holdTimeToLongJump = 0.2f;


    [SerializeField] private float _longJumpForce = 12f;
    [SerializeField] private float _shortJumpForce = 8f;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;
    [SerializeField] private bool _isVelocityMode = false;


    private bool _isJumpHeld = false;
    private float _jumpHeldTime = 0;
    private float _gravityAbs;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _movementHandler = GetComponent<CharacterMovementHandler>(); // Получаем ссылку на CharacterMovementHandler
    }

    private void Start()
    {
        _gravityAbs = Mathf.Abs(Physics2D.gravity.y);
    }

    public void Jump()
    {
        if (_movementHandler.IsGrounded()) // Используем метод из CharacterMovementHandler
        {
            _rb.linearVelocityY = _shortJumpForce;
            OnJump?.Invoke();
            _isJumpHeld = true;
            _jumpHeldTime = 0;
        }
    }

    public void CancelLongJump()
    {
        _isJumpHeld = false;
        _jumpHeldTime = 0;
    }

    private void CheckLongJump()
    {
        if (_jumpHeldTime >= _holdTimeToLongJump)
        {
            _rb.linearVelocityY = _longJumpForce;
            OnJump?.Invoke();
            _isJumpHeld = false;
        }
    }

    private void CheckHoldTime()
    {
        if (_isJumpHeld)
        {
            _jumpHeldTime += Time.deltaTime;
            CheckLongJump();
        }
    }

    private void Update()
    {
        CheckHoldTime();
    }

    private void FixedUpdate()
    {
        if (!_movementHandler.IsGrounded())
        {
            ApplyCustomGravity();
        }
    }

    private void ApplyCustomGravity()
    {
        if (_isVelocityMode)
        {
            if (_rb.linearVelocityY < 0)
            {
                _rb.linearVelocityY += Physics2D.gravity.y * (_fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (_rb.linearVelocityY > 0)
            {
                _rb.linearVelocityY += Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.fixedDeltaTime;
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
