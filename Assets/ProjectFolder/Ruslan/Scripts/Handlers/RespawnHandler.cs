using System;
using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    public Action OnRespawn;
    [SerializeField] private CharacterRespown _characterRespown;
    [SerializeField] private Transform _initialSpownPosition;
    private CharacterMoveController _characterController;
    private CharacterAnimationController _characterAnimationController;
    private Transform _characterTransform;
    private DeathHandler _deathHandler;
    private Animator _animator;
    private Vector2 _respawnPosition;

    private void Awake()
    {
        _deathHandler = GetComponent<DeathHandler>();
        _characterRespown = FindFirstObjectByType<CharacterRespown>();
        _animator = _characterRespown.GetComponentInChildren<Animator>();
        _characterController = _characterRespown.GetComponent<CharacterMoveController>();
        _characterAnimationController = _animator.GetComponent <CharacterAnimationController>();
    }
    private void OnEnable()
    {
        _deathHandler.OnDeath += RespawnCharacter;
        _characterRespown.OnRespownPoindFound += SetRespanPoint;
        _characterAnimationController.OnDeathAnimCompleat += RespawnCharacter;
        _characterAnimationController.OnRespawnAnimCompleat += ReturnControl;
    }

    private void OnDisable()
    {
        _deathHandler.OnDeath -= RespawnCharacter;
        _characterRespown.OnRespownPoindFound -= SetRespanPoint;
        _characterAnimationController.OnDeathAnimCompleat -= RespawnCharacter;
        _characterAnimationController.OnRespawnAnimCompleat -= ReturnControl;
    }

    private void Start()
    {
        _respawnPosition = _initialSpownPosition.position;
        _deathHandler = GetComponentInChildren<DeathHandler>();
        _characterTransform = _characterRespown.transform;
    }
    public void RespawnCharacter()
    {
        // рср бяе врн днкфмн опнхяундхрэ опх пеяоюбме лнфмн янгдюрэ йнпсрхмс еякх врн-рн онщрюомн мюдн ядекюрэ
        _characterTransform.position = _respawnPosition;
        _characterController.enabled = true;
        OnRespawn?.Invoke();
        _animator.SetTrigger("respawn");
        _characterTransform.GetComponent<CharacterDeath>().Resurrect();
    }

    public void SetRespanPoint(Vector2 newRespownPoint)
    {
        _respawnPosition = newRespownPoint;
    }

    private void ReturnControl()
    {
        _characterController.enabled = true;
    }
}
