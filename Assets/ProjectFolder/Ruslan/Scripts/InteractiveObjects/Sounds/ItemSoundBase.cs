using UnityEngine;

public abstract class ItemSoundBase : MonoBehaviour
{
    protected AudioSource _audioSource;
    virtual protected void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}
