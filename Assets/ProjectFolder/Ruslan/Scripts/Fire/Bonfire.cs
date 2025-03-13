using System.Collections;
using UnityEngine;

public class Bonfire : FireBase
{
    [SerializeField] private float _burningTime = 30;
    private Coroutine _burningCoroutine;
    public override void HandleFire(bool isFireStarterBurning)
    {
        if (isFireStarterBurning)
        {
            if (_burningCoroutine != null)
            {
                StopCoroutine(_burningCoroutine);
            }
            _burningCoroutine = StartCoroutine(Burn());
        }
    }

    private IEnumerator Burn()
    {
        _isBurning = true;
        _fire.SetActive(true);

        Vector2 initialScale = Vector2.one; // Начальный размер
        float elapsedTime = 0f;

        while (elapsedTime < _burningTime)
        {
            float progress = elapsedTime / _burningTime; // 0 -> 1
            _fire.transform.localScale = Vector2.Lerp(initialScale, Vector3.zero, progress);
            elapsedTime += Time.deltaTime;
            yield return null; // Ждем следующий кадр
        }

        _fire.transform.localScale = Vector3.zero; // Убеждаемся, что оно исчезло
        _isBurning = false;
        _fire.SetActive(false);
    }
}
