using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText; // ������ �� UI ����� ��� ����������� �������
    private Timer _timer; // ������ �� ������

    void Start()
    {
        _timer = FindFirstObjectByType<Timer>(); // ������� ������ � �����
        if (_timer != null)
        {
            _timer.OnTimeChanged += UpdateTimerText; // ������������� �� ������� ��������� �������
        }
    }

    void OnDestroy()
    {
        if (_timer != null)
        {
            _timer.OnTimeChanged -= UpdateTimerText; // ������������ �� ������� ��� ����������� �������
        }
    }

    private void UpdateTimerText(int minutes, int seconds)
    {
        // ����������� ����� � ������� �� UI
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
