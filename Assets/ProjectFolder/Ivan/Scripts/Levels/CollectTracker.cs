using UnityEngine;

public class CollectTracker : MonoBehaviour
{
    [SerializeField] private CollectHandler _collectHandler; // Ссылка на CollectHandler

    private int _currentCollected; // Текущее количество собранных предметов

    private void OnEnable()
    {
        if (_collectHandler != null)
        {
            _collectHandler.OnCollectValueChanged += HandleCollectUpdate;
        }
    }

    private void OnDisable()
    {
        if (_collectHandler != null)
        {
            _collectHandler.OnCollectValueChanged -= HandleCollectUpdate;
        }
    }

    // Обработчик события изменения количества предметов
    private void HandleCollectUpdate(int collectedCount)
    {
        _currentCollected = collectedCount;
        Debug.Log($"Собрано предметов: {_currentCollected}");      
    }

    // Метод для получения текущего количества (если нужно из другого скрипта)
    public int GetCurrentCollected()
    {
        return _currentCollected;
    }
}