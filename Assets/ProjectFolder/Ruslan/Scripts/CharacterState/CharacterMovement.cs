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
    private float _gravity;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void GroundMovement(float direction)
    {
        _rb.linearVelocityX = direction * _moveSpeed;
    }

    public void AirMovement(float direction)
    {
        _rb.AddForce(new Vector2(direction * _airMoveSpeed, 0), ForceMode2D.Force);
    }

    public void Jump()
    {
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        Debug.Log("Jump");
    }
        
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }

    public void GrabLedge()
    {
        _rb.linearVelocity = Vector2.zero;
        _rb.gravityScale = 0;
    }

    public void ClimbLedge()
    {
        StartCoroutine(ClimbRoutine());
    }

    public void DropLedge()
    {
        _rb.gravityScale = 1;
    }

    private IEnumerator ClimbRoutine()
    {
        float duration = 0.3f; // ƒлительность подт€гивани€
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(1, 1.7f, 0); // —мещаем вверх
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
            yield return null;
        }
    }
}
