using UnityEngine;
using TMPro;

public class DeathCounterUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DeathCounter _deathCounter;
    [SerializeField] private TMP_Text _deathText; // Ссылка на TextMeshPro компонент

    [Tooltip("Автоматически обновлять UI при изменении счетчика")]
    [SerializeField] private bool _autoUpdate = true;

    private void Awake()
    {
        // Автопоиск DeathCounter если не задан
        if (_deathCounter == null)
        {
            _deathCounter = FindObjectOfType<DeathCounter>();
        }

        if (_deathCounter == null)
        {
            Debug.LogError("DeathCounterUI: отсутствует ссылка на DeathCounter!", this);
            enabled = false;
            return;
        }

        // Проверка текстового поля
        if (_deathText == null)
        {
            Debug.LogError("DeathCounterUI: Отсутствует текстовая ссылка!", this);
            enabled = false;
            return;
        }

        // Первоначальное обновление
        UpdateDeathText();

        // Подписка на события если нужно автообновление
        if (_autoUpdate)
        {
            _deathCounter.OnDeathAdded += UpdateDeathText;
        }
    }

    private void OnDestroy()
    {
        // Отписка при уничтожении
        if (_deathCounter != null && _autoUpdate)
        {
            _deathCounter.OnDeathAdded -= UpdateDeathText;
        }
    }

    // Обновление текста UI
    public void UpdateDeathText()
    {
        _deathText.text = _deathCounter.GetTotalDeaths().ToString();           
    }
}