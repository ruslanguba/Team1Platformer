using System.Collections;
using TMPro;
using UnityEngine;

public class HintSystem : MonoBehaviour
{
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private TMP_Text hintText;
    [SerializeField] private float typingSpeed = 0.05f; // Скорость появления букв
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
        hintText.text = "";

        foreach (char letter in message)
        {
            hintText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(_hideDelay);
        HideHint();
    }
}
