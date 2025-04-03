using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingEffect : MonoBehaviour
{
    [SerializeField] private float _minDelay = 0.3f; // Минимальное время между молниями
    [SerializeField] private float _maxDelay = 8f; // Максимальное время между молниями
    [SerializeField] private float _flashDuration = 0.1f; // Длительность вспышки

    private Light2D _lightningLight; // Источник света
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _thunderSound;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _lightningLight = GetComponent<Light2D>();
        _lightningLight.enabled = false;
        StartCoroutine(LightningRoutine());
    }

    IEnumerator LightningRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minDelay, _maxDelay)); // Ждем случайное время

            // Основная вспышка
            _lightningLight.enabled = true;
            yield return new WaitForSeconds(_flashDuration);
            _lightningLight.enabled = false;
            float thunderDelayCoeff = 1;
            // Дополнительные вспышки (редко)
            if (Random.value > 0.5f)
            {
                yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
                _lightningLight.enabled = true;
                yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
                _lightningLight.enabled = false;
                thunderDelayCoeff = 0.5f;
            }
            float thunderDelay = Random.Range(0.8f, 1.5f) * thunderDelayCoeff;
            yield return new WaitForSeconds(thunderDelay);

            _audioSource.volume = Random.Range(0.7f, 1);
            _audioSource.pitch = Random.Range(0.8f, 1.2f);

            _audioSource.PlayOneShot(_thunderSound);

        }
    }
}
