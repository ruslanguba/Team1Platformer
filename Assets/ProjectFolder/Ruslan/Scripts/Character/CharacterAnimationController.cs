using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private Collider2D _pushCollider;
    private CharacterInterractor _characterInterractor;
    private CharacterMovementHandler _characterMovementHandler;
    private Transform _pullObject;
    private Rigidbody2D _rb;
    private Animator _animator;
    private CharacterJump _characterJump;
    private bool _isPulling;

    private void Awake()
    {
        _characterJump = GetComponent<CharacterJump>();
        _characterInterractor = GetComponent<CharacterInterractor>();
        _characterMovementHandler = GetComponent<CharacterMovementHandler>();
    }

    private void OnEnable()
    {
        _characterJump.OnJump += HandleJump;
        _characterInterractor.OnConnect += SetPullAnim;
        _characterInterractor.OnDisconnect += ResetPullAnim;
    }
    private void OnDisable()
    {
        _characterJump.OnJump -= HandleJump;
        _characterInterractor.OnConnect -= SetPullAnim;
        _characterInterractor.OnDisconnect -= ResetPullAnim;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _animator.SetBool("isPulling", false);
    }

    private void Update()
    {
        HandleRunAnimation();
        HandleFalling();
        CheckPushOrPull();
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

    private void SetPullAnim(Transform pullObject)
    {
        _pullObject = pullObject;
        _isPulling = true;
        //_characterMovementHandler.LockFlip();
        Debug.Log("SET isPulling = true");
        _animator.SetBool("isPulling", true);
    }

    private void ResetPullAnim()
    {
        _pullObject = null;
        _isPulling = false;
        //_characterMovementHandler.UnlockFlip();
        Debug.Log("SET isPulling = false");
        _animator.SetBool("isPulling", false);
        _animator.SetBool("isPushing", false);
        _animator.ResetTrigger("jump");
    }
    private void CheckPushOrPull()
    {
        if (_isPulling)
        {
            Vector2 directionToObject = (_pullObject.position - transform.position).normalized;
            Vector2 movementDirection = _rb.linearVelocity.normalized;

            float dot = Vector2.Dot(movementDirection, directionToObject);

            if (dot > 0.1f) // Если скалярное произведение положительное, движемся к объекту
            {
                Debug.Log("Moving TOWARDS pullObject");
                _animator.SetBool("isPulling", false);
                _animator.SetBool("isPushing", true);
            }
            else if (dot < -0.1f) // Если отрицательное — движемся в противоположную сторону
            {
                Debug.Log("Moving AWAY from pullObject");
                _animator.SetBool("isPulling", true);
                _animator.SetBool("isPushing", false);
            }
            else
            {
                Debug.Log("Standing still or moving sideways");
            }
        }
    }
}
