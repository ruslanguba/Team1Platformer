using UnityEngine;
using System;

public class DeathCounter : MonoBehaviour
{
    // ������� ��� ���������� ������
    public event Action OnDeathAdded;

    [Header("���������")]
    [Tooltip("������������� ������ CharacterDeath")]
    [SerializeField] private bool _autoFindCharacterDeath = true;

    [Header("������")]
    [SerializeField] private CharacterDeath _characterDeath;

    private int _totalDeaths = 0;
    private int _fireDeaths = 0;
    private int _instantDeaths = 0;


    private void Awake()
    {
        if (_autoFindCharacterDeath)
        {
            _characterDeath = FindFirstObjectByType<CharacterDeath>();
        }

        if (_characterDeath == null)
        {
            Debug.LogError("DeathCounter: ����������� ������ �� CharacterDeath!", this);
            return;
        }

        // ������������� �� ������� ������
        _characterDeath.OnDeathTriggerEntered += HandleDeath;
    }

    private void OnDestroy()
    {
        // ����� ���������� ��� ����������� �������
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

        // �������� �������
        OnDeathAdded?.Invoke();
        DebugDeathStats();
    }

    private void DebugDeathStats()
    {
        Debug.Log($"������! �����: {_totalDeaths} (�� ����: {_fireDeaths}, ����������: {_instantDeaths})");
    }

    // ������ ��� ��������� ����������
    public int GetTotalDeaths() => _totalDeaths;
    public int GetFireDeaths() => _fireDeaths;
    public int GetInstantDeaths() => _instantDeaths;

    // ����� ��� ������ ��������
    public void ResetCounter()
    {
        _totalDeaths = 0;
        _fireDeaths = 0;
        _instantDeaths = 0;
    }
}
