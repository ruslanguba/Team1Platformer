using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimerDisplay : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();

        // Автоматически находим таймер в сцене
        Timer timer = FindObjectOfType<Timer>();


        if (timer != null)
        {
            timer.OnTimeChanged += UpdateTimerDisplay;
        }
    }

    private void OnDestroy()
    {
        Timer timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.OnTimeChanged -= UpdateTimerDisplay;
        }
    }

    private void UpdateTimerDisplay(int minutes, int seconds)
    {
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
