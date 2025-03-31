using UnityEngine;
using System;

public class DeathCounter : MonoBehaviour
{
    // Событие при добавлении смерти
    public event Action OnDeathAdded;

    [Header("Настройки")]
    [Tooltip("Автоматически искать CharacterDeath")]
    [SerializeField] private bool _autoFindCharacterDeath = true;

    [Header("Ссылки")]
    [SerializeField] private CharacterDeath _characterDeath;

    private int _totalDeaths = 0;
    private int _fireDeaths = 0;
    private int _instantDeaths = 0;


    private void Awake()
    {
        if (_autoFindCharacterDeath)
        {
            _characterDeath = FindObjectOfType<CharacterDeath>();
        }

        if (_characterDeath == null)
        {
            Debug.LogError("DeathCounter: Отсутствует ссылка на CharacterDeath!", this);
            return;
        }

        // Подписываемся на событие смерти
        _characterDeath.OnDeathTriggerEntered += HandleDeath;
    }

    private void OnDestroy()
    {
        // Важно отписаться при уничтожении объекта
        if (_characterDeath != null)
        {
            _characterDeath.OnDeathTriggerEntered -= HandleDeath;
        }
    }

    private void HandleDeath(bool isInstantDeath)
    {
        _totalDeaths++;

        if (isInstantDeath)
        {
            _instantDeaths++;
        }
        else
        {
            _fireDeaths++;
        }

        // Вызываем событие
        OnDeathAdded?.Invoke();
        DebugDeathStats();
    }

    private void DebugDeathStats()
    {
        Debug.Log($"Смерть! Всего: {_totalDeaths} (От огня: {_fireDeaths}, Мгновенная: {_instantDeaths})");
    }

    // Методы для получения статистики
    public int GetTotalDeaths() => _totalDeaths;
    public int GetFireDeaths() => _fireDeaths;
    public int GetInstantDeaths() => _instantDeaths;

    // Метод для сброса счетчика
    public void ResetCounter()
    {
        _totalDeaths = 0;
        _fireDeaths = 0;
        _instantDeaths = 0;
    }


}
