using System;
using UnityEngine;

public class CollectHandler : MonoBehaviour
{
    public event Action<int> OnCollectValueChanged;
    private CharacterCollect _characterCollect;
    private int _collectedBananas;

    private void Awake()
    {
        _characterCollect = FindFirstObjectByType<CharacterCollect>();
    }

    private void OnEnable()
    {
        _characterCollect.OnCollect += CollectBanana;
    }

    private void OnDisable()
    {
        _characterCollect.OnCollect -= CollectBanana;
    }

    private void CollectBanana()
    {
        OnCollectValueChanged?.Invoke(_collectedBananas);
        _collectedBananas++;
    }
}
