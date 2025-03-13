using System.Collections;
using UnityEngine;

public class ImageHint : HintBase
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _showDuration;
    [SerializeField] private Color _color;
    [SerializeField] private Coroutine _coroutine;

    private void Start()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _color = _sprite.color;
        HideImage();
    }

    public override void ShowHint(float duration)
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _showDuration = duration;
        _coroutine = StartCoroutine(SetHintVisible());
    }

    private IEnumerator SetHintVisible()
    {
        ShowImage();
        // Плавное уменьшение прозрачности
        float elapsedTime = 0f;
        while (elapsedTime < _showDuration)
        {
            float progress = elapsedTime / _showDuration;
            Color currentColor = _sprite.color;
            currentColor.a = Mathf.Lerp(1f, 0f, progress); // Линейно интерполируем от 1 до 0
            _sprite.color = currentColor;

            elapsedTime += Time.deltaTime;
            yield return null; // Ждем следующий кадр
        }

        HideImage();
    }

    private void HideImage()
    {
        _color.a = 0; // Устанавливаем начальную альфу как 1 (полностью видимый)
        _sprite.color = _color;
    }

    private void ShowImage()
    {
        _color.a = 1;
        _sprite.color = _color;
    }
}
