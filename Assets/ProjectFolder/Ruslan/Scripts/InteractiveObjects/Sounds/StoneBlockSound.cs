using UnityEngine;

public class StoneBlockSound : ItemSoundBase
{
    [SerializeField] private AudioClip _fallInWater;
    [SerializeField] private AudioClip _fallOnGround;
    private Rigidbody2D _rb;
    private bool _isMoving;
    private bool _isFalling;
    private bool _isInWater;

    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    private void CheckMoving()
    {
        if(Mathf.Abs(_rb.linearVelocityX) > 0.2f)
        {
            _isMoving = true;
        }
    }

    private void CheckFalling()
    {
        if (_rb.linearVelocityY < -2)
        {
            _isFalling = true;
        }
    }

    private bool IsFalling()
    {
        return _rb.linearVelocityY < -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out FireStopper watter))
        {
            _isInWater = true;
        }
        if (IsFalling() && _isInWater)
        {
            _audioSource.PlayOneShot(_fallInWater);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(IsFalling() && !_isInWater)
        {
            _audioSource.PlayOneShot(_fallOnGround);
        }
    }
}
