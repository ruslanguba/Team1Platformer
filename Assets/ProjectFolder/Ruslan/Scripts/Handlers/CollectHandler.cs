using System;
using UnityEngine;

public class CollectHandler : MonoBehaviour 
{
    public event Action<int> OnCollectValueChanged;
    private CharacterCollect _characterCollect;
    private int _collectedValue;

    private void Awake()
    {
        _characterCollect = FindFirstObjectByType<CharacterCollect>();
    }

    public void SetCharacter(CharacterCollect characterCollect)
    {
        _characterCollect = characterCollect;
        _characterCollect.OnCollect += CollectBanana;
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
        _collectedValue++;
        OnCollectValueChanged?.Invoke(_collectedValue);
    }
}
