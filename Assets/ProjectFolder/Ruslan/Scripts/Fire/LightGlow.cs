using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightGlow : MonoBehaviour
{
    [SerializeField] Light2D _light;
    [SerializeField] float _intensity;
    [SerializeField] float _frequency;

    private void Start()
    {
        _light = GetComponent<Light2D>();
    }
    void Update()
    {
        _light.intensity = Mathf.PerlinNoise(Time.time * _frequency, 0f) * _intensity;
    }

    //[SerializeField] private Light2D _light => GetComponent<Light2D>();
    //[SerializeField] private float _changeDuration;
    //[SerializeField] private float _minRed = 0.8f, _maxRed = 1f;
    //[SerializeField] private float _minGreen = 0.4f, _maxGreen = 0.6f;
    //[SerializeField] private float _minBlue = 0.1f, _maxBlue = 0.3f;
    //[SerializeField] private float _minRadius = 3, _maxRadius = 4;
    //[SerializeField] private float _minFalloff = 0.4f, _maxFalloff = 0.7f;
    //private Coroutine _glowCoroutine;

    //private void OnEnable()
    //{
    //    if (_glowCoroutine == null)
    //    {
    //        _glowCoroutine = StartCoroutine(GlowEffect());
    //    }
    //}

    //private void OnDisable()
    //{
    //    if (_glowCoroutine != null)
    //    {
    //        StopCoroutine(_glowCoroutine);
    //        _glowCoroutine = null;
    //    }
    //}

    //private IEnumerator GlowEffect()
    //{
    //    while (true)
    //    {
    //        Color startColor = _light.color;
    //        Color targetColor = GetRandomColor();

    //        float startRadius = _light.pointLightOuterRadius;
    //        float targetRadius = Random.Range(_minRadius, _maxRadius);

    //        float startFalloff = _light.falloffIntensity;
    //        float targetFalloff = Random.Range(_minFalloff, _maxFalloff);

    //        float elapsedTime = 0f;

    //        _changeDuration = Random.Range(0.5f, 1);
    //        while (elapsedTime < _changeDuration)
    //        {
    //            elapsedTime += Time.deltaTime;
    //            float t = elapsedTime / _changeDuration;

    //            _light.color = Color.Lerp(startColor, targetColor, t);
    //            _light.pointLightOuterRadius = Mathf.Lerp(startRadius, targetRadius, t);
    //            _light.falloffIntensity = Mathf.Lerp(startFalloff, targetFalloff, t);

    //            yield return null;
    //        }

    //        yield return new WaitForSeconds(_changeDuration);
    //    }
    //}

    //private Color GetRandomColor()
    //{
    //    float r = Random.Range(_minRed, _maxRed);
    //    float g = Random.Range(_minGreen, _maxGreen);
    //    float b = Random.Range(_minBlue, _maxBlue);
    //    return new Color(r, g, b);
    //}

}
