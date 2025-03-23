using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private Collider2D _pushCollider;
    private Rigidbody2D _rb;
    private Animator _animator;
    private CharacterJump _characterJump;
    private bool _isFalling;

    private void Awake()
    {
        _characterJump = GetComponent<CharacterJump>();
    }

    private void OnEnable()
    {
        _characterJump.OnJump += HandleJump;
    }
    private void OnDisable()
    {
        _characterJump.OnJump -= HandleJump;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleRunAnimation();
        HandleFalling();
    }

    private void HandleRunAnimation()
    {
        float speed = Mathf.Abs(_rb.linearVelocity.x); // Берем модуль скорости по X
        _animator.SetFloat("speed", speed); // Передаем скорость в Animator
    }

    private void HandleJump()
    {
        _animator.SetTrigger("jump");
    }

    private void HandleFalling()
    {
        float speedY = _rb.linearVelocity.y;
        _animator.SetFloat("speedY", speedY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.otherCollider == _pushCollider && collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            _animator.SetBool("isPushing", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.otherCollider == _pushCollider && collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            _animator.SetBool("isPushing", false);
        }
    }
}
