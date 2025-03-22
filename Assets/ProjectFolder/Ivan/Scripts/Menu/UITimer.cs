using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText; // Ссылка на UI текст для отображения времени
    private Timer _timer; // Ссылка на таймер

    void Start()
    {
        _timer = FindObjectOfType<Timer>(); // Находим таймер в сцене
        if (_timer != null)
        {
            _timer.OnTimeChanged += UpdateTimerText; // Подписываемся на событие изменения времени
        }
    }

    void OnDestroy()
    {
        if (_timer != null)
        {
            _timer.OnTimeChanged -= UpdateTimerText; // Отписываемся от события при уничтожении объекта
        }
    }

    private void UpdateTimerText(int minutes, int seconds)
    {
        // Форматируем текст и выводим на UI
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
