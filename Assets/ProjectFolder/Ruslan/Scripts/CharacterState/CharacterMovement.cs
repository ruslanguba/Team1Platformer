using System;
using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private float _airMoveSpeed = 5;
    [SerializeField] private float _doubleJumpForce = 5;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private bool _isFacingRight = true;

    [SerializeField] private Transform _wallCheckPivot;
    [SerializeField] private float _wallCheckRadius = 0.1f;
    [SerializeField] private float _wallSlideSpeed = 1; // Сила замедления при скольжении
    private bool _isWallSliding;

    private Rigidbody2D _rb;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public bool IsWallSliding()
    {
        return Physics2D.OverlapCircle(_wallCheckPivot.position, _wallCheckRadius, _groundLayer);
    }

    public void GroundMovement(float direction)
    {
        if(GROUND_VELOCITY_MODE)
        {
            _rb.linearVelocityX = direction * _moveSpeed;
        }

        else if(!GROUND_VELOCITY_MODE)
        {
            _rb.AddForce(new Vector2(direction * _moveSpeed, 0), ForceMode2D.Force);
            if(direction == 0)
                _rb.linearVelocityX = 0;
        }

        CheckDirection(direction );
    }

    public void AirMovement(float direction)
    {
        if (direction != 0)
            if (AIR_VELOCITY_MODE)
            {
                _rb.linearVelocity = new Vector2(direction * _airMoveSpeed, _rb.linearVelocity.y);
            }
            else if (!AIR_VELOCITY_MODE)
            {
                _rb.AddForce(new Vector2(direction * _airMoveSpeed, 0), ForceMode2D.Force);
            }
        CheckDirection(direction);
    }

    public void WallSliding()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, -_wallSlideSpeed);
    }

    public void Jump()
    {
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    public void DoubleJump()
    {
        if (DOUBLE_JUMP_ENABLE)
        {
            if(_rb.linearVelocity.y < 0)
            {
                _rb.linearVelocityY = 0;
            }
            _rb.AddForce(Vector2.up * _doubleJumpForce, ForceMode2D.Impulse);
        }
    }

    public void WallJump()
    {
        float direction = transform.localScale.x;
        _rb.AddForce(new Vector2(_jumpForce * - direction, _jumpForce), ForceMode2D.Impulse);
        FlipDirection();
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

    public void CheckWallSlide()
    {
        _isWallSliding = IsWallSliding();
    }

    //////////////////////////////////////////////////////////////// TEST /////////////////////////////////////////////////////////////////////

    bool GROUND_VELOCITY_MODE = true;
    bool AIR_VELOCITY_MODE = true;
    bool DOUBLE_JUMP_ENABLE = true;
  
    public void SetMoveSpeed(float speed)
    {
        _moveSpeed = speed;
    }

    public void SetAirSpeed(float speed)
    {
        _airMoveSpeed = speed;
    }

    public void SetJumpForce(float force)
    {
        _jumpForce = force;
    }

    public void SetDoubleJumpForce(float force)
    {
        _doubleJumpForce = force;
    }

    public bool GET_GROUND_VELOCITY_MODE()
    {
        return DOUBLE_JUMP_ENABLE;
    }

    public bool GET_AIR_VELOCITY_MODE()
    {
        return AIR_VELOCITY_MODE;
    }
    public bool GET_DOUBLE_JUMP_ENABLE()
    {
        return DOUBLE_JUMP_ENABLE;
    }

    public float GetMoveSpeed()
    {
        return _moveSpeed;
    }
    public float GetAirSpeed()
    {
        return _airMoveSpeed;
    }
    public float GetJumpForce()
    {
        return _jumpForce;
    }
    public float GetDoubleJumpForce()
    {
        return _doubleJumpForce;
    }

    public void SET_GROUND_VELOCITY_MODE(bool value)
    {
        GROUND_VELOCITY_MODE = value;
    }

    public void SET_AIR_VELOCITY_MODE(bool value)
    {
        AIR_VELOCITY_MODE = value;
    }

    internal void SET_DOUBLE_JUMP_ENABLE(bool value)
    {
        DOUBLE_JUMP_ENABLE = value;
    }
}
