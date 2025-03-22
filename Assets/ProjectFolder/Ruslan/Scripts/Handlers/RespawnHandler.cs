using UnityEngine;
using UnityEngine.TextCore.Text;

public class RespawnHandler
{
    [SerializeField] private CharacterRespown _characterRespown;
    private Transform _characterTransform;
    private DeathHandler _deathHandler;
    private Vector2 _respawnPosition;

    public RespawnHandler(Transform character, Vector2 initialRespawnPosition, DeathHandler deathHandler)
    {
        _characterTransform = character;
        _respawnPosition = initialRespawnPosition;
        _deathHandler = deathHandler;
        Initialize();
    }

    private void Initialize()
    {
        _characterRespown = _characterTransform.GetComponent<CharacterRespown>();
        _deathHandler.OnDeath += RespawnCharacter;
        _characterRespown.OnRespownPoindFound += SetRespanPoint;
    }

    public void Unsubscribe()
    {
        _deathHandler.OnDeath -= RespawnCharacter;
        _characterRespown.OnRespownPoindFound -= SetRespanPoint;
    }
    //private void Awake()
    //{
    //    _deathHandler = GetComponent<DeathHandler>();
    //    _characterRespown = FindFirstObjectByType<CharacterRespown>();
    //}
    //private void OnEnable()
    //{
    //    _deathHandler.OnDeath += RespawnCharacter;
    //    _characterRespown.OnRespownPoindFound += SetRespanPoint;
    //}

    //private void OnDisable()
    //{
    //    _deathHandler.OnDeath -= RespawnCharacter;
    //}

    //private void Start()
    //{
    //    _respawnPosition = _initialSpownPosition.position;
    //    _deathHandler = GetComponentInChildren<DeathHandler>();
    //    _characterTransform = _characterRespown.transform;
    //}
    public void RespawnCharacter()
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
