using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        HandleRunAnimation();
    }
    private void HandleRunAnimation()
    {
        float speed = Mathf.Abs(_rb.linearVelocity.x); // Берем модуль скорости по X
        _animator.SetFloat("velocity", speed); // Передаем скорость в Animator
    }
}
