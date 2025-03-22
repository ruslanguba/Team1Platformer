using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightHandler : MonoBehaviour
{
    [SerializeField] private Light2D _globalLigth;
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _spiritualColor;
    private Coroutine _coroutine;
    private CharacterHintActivator _characterHintActivator;


    public GlobalLightHandler(Transform character, GlobalLightHintSettings colorSettings, Light2D light) 
    {
        _characterHintActivator = character.GetComponent<CharacterHintActivator>();
        _normalColor = colorSettings.NormalColor;

    }
    private void Awake()
    {
        _characterHintActivator = FindFirstObjectByType<CharacterHintActivator>();
    }
    //public void SetCharacterHintActivator(CharacterHintActivator hintActivator)
    //{
    //    _characterHintActivator = hintActivator;
    //    _characterHintActivator.OnShowHint += SetSpiritLighting;
    //}
    private void OnEnable()
    {
        _characterHintActivator.OnShowHint += SetSpiritLighting;
    }

    private void OnDisable()
    {
        _characterHintActivator.OnShowHint -= SetSpiritLighting;
    }

    private void Start()
    {
        _globalLigth = GetComponent<Light2D>();
        _globalLigth.color = _normalColor;
    }

    public void SetSpiritLighting(float duration)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(SpiritLighting(duration));
    }

    private IEnumerator SpiritLighting(float duration)
    {
        // Сразу устанавливаем цвет на _spiritualColor
        _globalLigth.color = _spiritualColor;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            // Рассчитываем прогресс интерполяции
            float progress = elapsedTime / duration;

            // Интерполируем цвета от _spiritualColor к _normalColor
            _globalLigth.color = Color.Lerp(_spiritualColor, _normalColor, progress);

            elapsedTime += Time.deltaTime;
            yield return null; // Ждем следующий кадр
        }

        // Убедимся, что в конце цвета точно будут установлены как _normalColor
        _globalLigth.color = _normalColor;
    }
}
