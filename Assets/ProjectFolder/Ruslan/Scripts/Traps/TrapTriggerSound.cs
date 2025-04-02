using UnityEngine;

public class TrapTriggerSound : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out CharacterFire fire))
        {
            _audioSource.PlayOneShot(_audioClip);
        }
    }
}
