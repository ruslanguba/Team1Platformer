using System;
using UnityEngine;

public class CollectHandler : MonoBehaviour 
{
    public event Action<int> OnCollectValueChanged;
    private CharacterCollect _characterCollect;
    private int _collectedBananas;

    //public CollectHandler(CharacterCollect characterCollect)
    //{
    //    _characterCollect = characterCollect;
    //    _characterCollect.OnCollect += CollectBanana;
    //}
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

    //public void Unsubscribe()
    //{
    //    _characterCollect.OnCollect -= CollectBanana;
    //}
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
