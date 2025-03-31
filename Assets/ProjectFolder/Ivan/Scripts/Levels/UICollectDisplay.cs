using UnityEngine;
using TMPro; 

[RequireComponent(typeof(TextMeshProUGUI))] // Автоматически добавляет компонент, если его нет
public class UICollectDisplay : MonoBehaviour
{

    [Header("Ссылки")]
    [SerializeField] private CollectHandler _collectHandler; // Ссылка на CollectHandler
    private TextMeshProUGUI _collectText; // Компонент текста

    private void Awake()
    {
        // Получаем компонент TextMeshProUGUI
        _collectText = GetComponent<TextMeshProUGUI>();

        // Если ссылка на CollectHandler не задана в инспекторе, попробуем найти автоматически
        if (_collectHandler == null)
        {
            _collectHandler = FindObjectOfType<CollectHandler>();
            if (_collectHandler == null)
            {
                Debug.LogError("UICollectDisplay: CollectHandler не найден!");
                enabled = false; // Отключаем скрипт, чтобы избежать ошибок
                return;
            }
        }
    }

    private void OnEnable()
    {
        // Подписываемся на событие при включении
        if (_collectHandler != null)
        {
            _collectHandler.OnCollectValueChanged += UpdateCollectText;
        }
    }

    private void OnDisable()
    {
        // Отписываемся при выключении
        if (_collectHandler != null)
        {
            _collectHandler.OnCollectValueChanged -= UpdateCollectText;
        }
    }

    // Обновляем текст при изменении количества предметов
    private void UpdateCollectText(int collectedCount)
    {
        if (_collectText != null)
        {
            _collectText.text = collectedCount.ToString();
        }
    }
}