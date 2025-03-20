using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    [SerializeField] private Transform _initialSpownPosition;
    [SerializeField] private CharacterRespown _characterRespown;
    private Transform _characterTransform;
    private DeathHandler _deathHandler;
    private Vector2 _respawnPosition;

    private void Awake()
    {
        _deathHandler = GetComponent<DeathHandler>();
    }
    private void OnEnable()
    {
        _deathHandler.OnDeath += RespawnCharacter;
        _characterRespown.OnRespownPoindFound += SetRespanPoint;
    }
    private void OnDisable()
    {
        _deathHandler.OnDeath -= RespawnCharacter;
    }
    private void Start()
    {
        _respawnPosition = _initialSpownPosition.position;
        _deathHandler = GetComponentInChildren<DeathHandler>();
        _characterTransform = _characterRespown.transform;
    }
    private void RespawnCharacter()
    {
        // рср бяе врн днкфмн опнхяундхрэ опх пеяоюбме лнфмн янгдюрэ йнпсрхмс еякх врн-рн онщрюомн мюдн ядекюрэ
        _characterTransform.position = _respawnPosition;
        _characterTransform.GetComponent<PlayerInputHandler>().enabled = true;
    }

    public void SetRespanPoint(Vector2 newRespownPoint)
    {
        _respawnPosition = newRespownPoint;
    }
}
