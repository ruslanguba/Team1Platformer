using System.Collections;
using UnityEngine;

public class CharacterSoundController : MonoBehaviour
{

    [SerializeField] private float _stepInterval = 0.4f;
    [SerializeField] private Surface _defoultLevelSurface;

    [Header("Grass Sounds")]
    [SerializeField] private AudioClip[] _grassStep;
    [SerializeField] private AudioClip[] _grassJump;
    [Header("Ground Sounds")]

    [SerializeField] private AudioClip[] _groundStep;
    [SerializeField] private AudioClip[] _groundJump;
    [Header("Water Sounds")]

    [SerializeField] private AudioClip[] _waterStep;
    [SerializeField] private AudioClip[] _waterJump;
    [Header("Stone Sounds")]

    [SerializeField] private AudioClip[] _stoneStep;
    [SerializeField] private AudioClip[] _stoneJump;
    [Header("Wood Sounds")]

    [SerializeField] private AudioClip[] _woodStep;
    [SerializeField] private AudioClip[] _woodJump;
    [Header("Fire Sounds")]

    [SerializeField] private AudioClip _fireOn;
    [SerializeField] private AudioClip _fireOff;
    [Header("Death Sounds")]

    [SerializeField] private AudioClip _death;
    [SerializeField] private AudioClip _fireDeath;
    private CharacterMovementHandler _movementHandler;
    private CharacterJump _characterJump;
    private CharacterFire _characterFire;
    private CharacterDeath _characterDeath;
    private Rigidbody2D _rb;
    private AudioSource _audioSource;
    private AudioClip[] _defaultStep;
    private AudioClip[] _defaultJump;
    private AudioClip _footStepSond;
    private AudioClip _jumpSound;
    private enum Surface {grass, ground, water, wood, stone}
    private Surface _currentSurface;
    private bool _isMoving;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _movementHandler = GetComponentInParent<CharacterMovementHandler>();
        _characterJump = GetComponentInParent<CharacterJump>();
        _characterFire = GetComponentInParent<CharacterFire>();
        _characterDeath = GetComponentInParent<CharacterDeath>();
    }

    private void OnEnable()
    {
        _characterJump.OnJump += PlayJumpClip;
        _characterFire.OnFire += PlayFireSound;
        _characterDeath.OnDeathTriggerEntered += PlayDeathSound;
    }

    private void OnDisable()
    {
        _characterJump.OnJump -= PlayJumpClip;
        _characterFire.OnFire -= PlayFireSound;
        _characterDeath.OnDeathTriggerEntered -= PlayDeathSound;
    }

    void Start()
    {
        StartCoroutine(PlayFootsteps());
        _currentSurface = Surface.grass;

        switch (_defoultLevelSurface)
        {
            case Surface.grass:
                _defaultStep = _grassStep;
                _defaultJump = _grassJump;
                break;
            case Surface.ground: 
                _defaultStep = _groundStep;
                _defaultJump = _groundJump;
                break;
            default:
                _defaultStep = _grassStep;
                _defaultJump = _grassJump;
                break;
        }
    }

    void Update()
    {
        _isMoving = Mathf.Abs(_rb.linearVelocityX) > 0.1f && _movementHandler.IsGrounded();
    }

    private AudioClip FootStepSound()
    {
        switch (_currentSurface)
        {
            case Surface.water:
                _footStepSond = _waterStep[Random.Range(0, _waterStep.Length)];
                break;
            case Surface.stone:
                _footStepSond = _stoneStep[Random.Range(0, _stoneStep.Length)];
                break;
            case Surface.wood:
                _footStepSond = _woodStep[Random.Range(0, _woodStep.Length)];
                break;
            case Surface.grass:
                _footStepSond = _grassStep[Random.Range(0, _grassStep.Length)];
                break;
            case Surface.ground:
                _footStepSond = _groundStep[Random.Range(0, _groundStep.Length)];
                break;
            default:
                _footStepSond = _defaultStep[Random.Range(0, _defaultStep.Length)];
                break;
        }
        return _footStepSond;
    }

    private AudioClip JumpSound()
    {
        switch (_currentSurface)
        {
            case Surface.water:
                _jumpSound = _waterJump[Random.Range(0, _waterJump.Length)];
                break;
            case Surface.stone:
                _jumpSound = _stoneJump[Random.Range(0, _stoneJump.Length)];
                break;
            case Surface.wood:
                _jumpSound = _woodJump[Random.Range(0, _woodJump.Length)];
                break;
            case Surface.grass:
                _jumpSound = _grassJump[Random.Range(0, _grassJump.Length)];
                break;
            case Surface.ground:
                _jumpSound = _groundJump[Random.Range(0, _groundJump.Length)];
                break;

            default:
                _jumpSound = _defaultJump[Random.Range(0, _defaultJump.Length)];
                break;
        }
        return _jumpSound;
    }

    private IEnumerator PlayFootsteps()
    {
        while (true)
        {
            if (_isMoving)
            {
                _audioSource.PlayOneShot(FootStepSound());
            }
            yield return new WaitForSeconds(_stepInterval);
        }
    }

    private void PlayJumpClip()
    {
        _audioSource.PlayOneShot(JumpSound());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out FireStopper fireStopper))
        {
            Debug.Log("Water");
            _currentSurface = Surface.water;
            _audioSource.PlayOneShot(_waterJump[Random.Range(0, _waterJump.Length)]);
            return;
        }
        if (collision.TryGetComponent(out FireBridgeTree woodBridge))
        {
            Debug.Log("Wood");
            _currentSurface = Surface.wood;
            return;
        }
        if (collision.TryGetComponent(out Stone stone) || collision.TryGetComponent(out Interactable interactable))
        {
            _currentSurface = Surface.stone;
            _audioSource.PlayOneShot(_stoneStep[Random.Range(0, _stoneStep.Length)]);
            return;
        }
        if (collision.TryGetComponent(out Grass grass))
        {
            _currentSurface = Surface.grass;
            return;
        }
        if (collision.TryGetComponent(out Ground ground))
        {
            _currentSurface = Surface.ground;
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _currentSurface = Surface.grass;
    }

    private void PlayFireSound(bool isFireOn) => _audioSource.PlayOneShot(isFireOn ? _fireOn : _fireOff);

    private void PlayDeathSound(bool isNotFire) => _audioSource.PlayOneShot(isNotFire ? _death : _fireDeath);
}
