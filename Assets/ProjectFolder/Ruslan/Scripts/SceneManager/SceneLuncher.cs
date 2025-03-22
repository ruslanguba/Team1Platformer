using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SceneLuncher : MonoBehaviour
{
    [SerializeField] private GameObject _character;
    [SerializeField] private Transform _intialSpawnPosition;
    [SerializeField] private CinemachineCamera _virtualCamera;
    [SerializeField] private Light2D _globalLight;
    [SerializeField] private GlobalLightHintSettings _globalLightHintSettings;
    [SerializeField] private CollectHandler _collectHandler; 
    private RespawnHandler _respawnHandler;
    private DeathHandler _deathHandler;
    private GlobalLightHandler _lightHandler;


    private void Start()
    {
        _lightHandler = FindFirstObjectByType<GlobalLightHandler>();
        _character = Instantiate(_character, _intialSpawnPosition.position, Quaternion.identity);
        _virtualCamera.Follow = _character.transform;
        _lightHandler.SetCharacterHintActivator(_character.GetComponent<CharacterHintActivator>());

        _deathHandler = new DeathHandler(_character.GetComponent<CharacterDeath>());
        _respawnHandler = new RespawnHandler(_character.transform, _intialSpawnPosition.position, _deathHandler);
        _lightHandler = new GlobalLightHandler(_character.transform, _globalLightHintSettings, _globalLight);
        _collectHandler.SetCharacter(_character.GetComponent<CharacterCollect>()); 
    }

    private void OnDestroy()
    {
        _deathHandler.Unsubscribe();
        _respawnHandler.Unsubscribe();
    }
}
