using System.Collections;
using TMPro;
using UnityEngine;

public class HintSystem : MonoBehaviour
{
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private TMP_Text hintText;
    [SerializeField] private float fadeDuration = 1; // Скорость появления букв
    [SerializeField] private float _hideDelay;
    private HintDetector _hintDetector;

    private Coroutine _typingCoroutine;

    private void Awake()
    {
        _hintDetector = FindFirstObjectByType<HintDetector>();
    }
    private void OnEnable()
    {
        _hintDetector.OnHintFound += ShowHint;
    }

    private void OnDisable()
    {
        _hintDetector.OnHintFound -= ShowHint;
    }

    public void ShowHint(string message)
    {
        hintPanel.SetActive(true);

        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
        }

        _typingCoroutine = StartCoroutine(TypeText(message));
    }

    public void HideHint()
    {
        if (_typingCoroutine != null)
        {
            StopCoroutine(_typingCoroutine);
        }

        hintPanel.SetActive(false);
        hintText.text = ""; 
    }

    private IEnumerator TypeText(string message)
    {
        hintText.text = message;
        Color textColor = hintText.color;

        // Плавное появление
        for (float t = 0; t < 1; t += Time.deltaTime / fadeDuration)
        {
            textColor.a = t;
            hintText.color = textColor;
            yield return null;
        }
        textColor.a = 1;
        hintText.color = textColor;

        // Ждём перед исчезновением
        yield return new WaitForSeconds(_hideDelay);

        // Плавное исчезновение
        for (float t = 1; t > 0; t -= Time.deltaTime / fadeDuration)
        {
            textColor.a = t;
            hintText.color = textColor;
            yield return null;
        }
        textColor.a = 0;
        hintText.color = textColor;

        HideHint();
    }
}
