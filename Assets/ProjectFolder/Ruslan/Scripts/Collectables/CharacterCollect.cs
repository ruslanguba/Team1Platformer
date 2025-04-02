using System;
using UnityEngine;

public class CharacterCollect : MonoBehaviour
{
    public event Action OnCollect;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private void Start()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out ICollectable collectable))
        {
            _audioSource.PlayOneShot(_audioClip);
            collectable.Collect();
            OnCollect?.Invoke();
        }
    }
}
