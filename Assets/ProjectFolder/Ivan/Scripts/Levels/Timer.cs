using System;
using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    [HideInInspector] public float TimeElapsed; // Время, прошедшее с начала отсчета
    private bool _isRunning; // Флаг, указывающий, запущен ли таймер

    public event Action<int, int> OnTimeChanged; // Событие для обновления времени (минуты, секунды)

    void Start()
    {
        TimeElapsed = 0f;
        _isRunning = true;
        StartCoroutine(UpdateTimerCoroutine()); // Запускаем корутину для обновления времени
    }

    private IEnumerator UpdateTimerCoroutine()
    {
        while (_isRunning)
        {
            TimeElapsed++; // Увеличиваем время на 1 секунду
            UpdateTimerText(); // Обновляем текст таймера
            yield return new WaitForSeconds(1f); // Ждем 1 секунду
        }
    }

    private void UpdateTimerText()
    {
        // Преобразуем время в минуты и секунды
        int minutes = Mathf.FloorToInt(TimeElapsed / 60);
        int seconds = Mathf.FloorToInt(TimeElapsed % 60);
        OnTimeChanged?.Invoke(minutes, seconds); // Уведомляем подписчиков об изменении времени
    }

    public void StopTimer()
    {
        _isRunning = false; // Останавливаем таймер
    }

    public void StartTimer()
    {
        _isRunning = true; // Запускаем таймер
    }
}
