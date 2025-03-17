using UnityEngine;

public class FireAnimationController : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    public void FlipSprite()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}
