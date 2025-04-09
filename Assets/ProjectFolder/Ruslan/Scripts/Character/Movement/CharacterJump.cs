using System;
using System.Collections;
using UnityEngine;

public class CharacterJump : MonoBehaviour, IJumpable
{
    public Action OnJump;
    private Rigidbody2D _rb;
    private CharacterMovementHandler _movementHandler; // Добавляем ссылку на CharacterMovementHandler


    [SerializeField] private int _jumpMaxCount = 1;
    [SerializeField] private float _firstJumpForce = 9;
    [SerializeField] private float _doubleJumpForc = 8;
    [SerializeField] private int _currentJumpsCount = 0;

    //[SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;
    [SerializeField] private bool _isVelocityMode = false;
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
        if (_movementHandler.IsGrounded() && _currentJumpsCount == 0) // Используем метод из CharacterMovementHandler
        {
            _currentJumpsCount++;
            _rb.linearVelocityY = _firstJumpForce;
            OnJump?.Invoke();
        }
        else if (_currentJumpsCount < _jumpMaxCount && !_movementHandler.IsGrounded())
        {
            DoubleJump();
        }
        StartCoroutine(CheckLanding());
    }

    public void DoubleJump()
    {
        if(_currentJumpsCount < _jumpMaxCount)
        {
            _currentJumpsCount++;

            _rb.linearVelocityY = _doubleJumpForc;
            OnJump?.Invoke();
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
        if(_isVelocityMode)
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
                if (_movementHandler.IsGrounded())
                {
                    _currentJumpsCount = 0;
                }
            }
            else if (_rb.linearVelocityY > 0)
            {
                _rb.AddForce(Vector2.down * (_lowJumpMultiplier - 1) * _gravityAbs, ForceMode2D.Force);
            }
        }
    }

    private IEnumerator CheckLanding()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.4f);
            if (_movementHandler.IsGrounded())
            {
                _currentJumpsCount = 0;
            }
        }
    }
}
