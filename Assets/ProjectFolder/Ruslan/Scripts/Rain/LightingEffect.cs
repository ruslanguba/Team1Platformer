using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingEffect : MonoBehaviour
{
    public Light2D lightningLight; // Источник света
    public float minDelay = 0.3f; // Минимальное время между молниями
    public float maxDelay = 8f; // Максимальное время между молниями
    public float flashDuration = 0.1f; // Длительность вспышки

    void Start()
    {
        StartCoroutine(LightningRoutine());
    }

    IEnumerator LightningRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay)); // Ждем случайное время

            // Основная вспышка
            lightningLight.enabled = true;
            yield return new WaitForSeconds(flashDuration);
            lightningLight.enabled = false;

            // Дополнительные вспышки (редко)
            if (Random.value > 0.5f)
            {
                yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
                lightningLight.enabled = true;
                yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));
                lightningLight.enabled = false;
            }
        }
    }
}
