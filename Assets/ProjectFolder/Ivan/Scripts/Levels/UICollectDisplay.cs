using UnityEngine;
using TMPro;

public class UICollectDisplay : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private CollectTracker _collectTracker;

    private TextMeshProUGUI _collectText;

    private void Awake()
    {
        _collectText = GetComponent<TextMeshProUGUI>();

        if (_collectTracker == null)
            _collectTracker = FindObjectOfType<CollectTracker>();
    }

    private void OnEnable()
    {
        if (_collectTracker != null)
        {
            _collectTracker.OnCollectedChanged += UpdateText;
            UpdateText(_collectTracker.GetCurrentCollected()); // Обновляем сразу
        }
    }

    private void OnDisable()
    {
        if (_collectTracker != null)
            _collectTracker.OnCollectedChanged -= UpdateText;
    }

    private void UpdateText(int amount)
    {
        if (_collectText != null)
            _collectText.text = amount.ToString();
    }
}