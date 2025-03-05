using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _jumpForce = 10;
    [SerializeField] private float _airMoveSpeed = 5;
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
        _rb.linearVelocityX = direction * _moveSpeed;
        CheckDirection(direction);
    }

    public void AirMovement(float direction)
    {
        _rb.AddForce(new Vector2(direction * _airMoveSpeed, 0), ForceMode2D.Force);
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
}
