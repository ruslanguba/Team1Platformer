using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CharacterHintActivator : MonoBehaviour
{
    public event Action<float> OnShowHint;
    [SerializeField] private float _checkRadius = 5;
    [SerializeField] private float _hintShowDuration;
    [SerializeField] private LayerMask _hintLayer;
    private PlayerInputHandler _inputHandler;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
    }

    private void OnEnable()
    {
        _inputHandler.OnHelpInput += ActivateHints;
    }

    private void ActivateHints()
    {
        OnShowHint?.Invoke(_hintShowDuration);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _checkRadius, _hintLayer);
        foreach (Collider2D collider in colliders) 
        {
            collider.TryGetComponent(out IHint hint);
            hint.ShowHint(_hintShowDuration);
        }
    }
}
