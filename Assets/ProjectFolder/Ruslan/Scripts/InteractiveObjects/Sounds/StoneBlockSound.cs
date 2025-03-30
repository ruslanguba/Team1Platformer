using UnityEngine;

public class StoneBlockSound : ItemSoundBase
{
    [SerializeField] private AudioClip _fallInWater;
    [SerializeField] private AudioClip _fallOnGround;
    [SerializeField] private AudioClip _groundMoveSound;
    [SerializeField] private AudioClip _waterMoveSound;
    private AudioClip _moveSound;
    private Rigidbody2D _rb;
    private bool _isMoving;
    private bool _isFalling;
    private bool _isInWater;
    private bool _isPlaying = false;

    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
        _moveSound = _groundMoveSound;
    }

    private void Update()
    {
        CheckMoving();
        PlayMoveSound();
    }

    private void CheckMoving()
    {
        _isMoving = Mathf.Abs(_rb.linearVelocityX) > 0.2f;
    }

    private void PlayMoveSound() 
    {
        if (_isMoving && !_audioSource.isPlaying)
        {
            _audioSource.clip = _moveSound;
            _audioSource.loop = true;
            _audioSource.Play();
        }
        else if (!_isMoving && _audioSource.isPlaying)
        {
            _audioSource.Pause();
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
            _moveSound = _waterMoveSound;
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
            float minSpeed = 0f;
            float maxSpeed = -5f; // Максимальная скорость падения (отрицательная)
            float fallSpeed = Mathf.Clamp(_rb.linearVelocityY, maxSpeed, minSpeed); // Ограничиваем диапазон
            float volume = Mathf.InverseLerp(minSpeed, maxSpeed, fallSpeed); // Приводим к 0-1
            _audioSource.pitch = Random.Range(0.9f, 1.1f);
            _audioSource.PlayOneShot(_fallOnGround, volume);
        }
    }
    public void StartSound()
    {
        _isPlaying = true;
    }

    // Выключить звук
    public void StopSound()
    {
        _isPlaying = false;
    }


}
