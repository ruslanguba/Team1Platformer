using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---AudioSource---")]
    public AudioSource MusicSource;
    public AudioSource SFXSource;

    [Header("---AudioClip---")]
    public AudioClip BackgroundMusic;

    private void Start()
    {
        if (MusicSource != null && BackgroundMusic != null)
        {
            MusicSource.clip = BackgroundMusic;
            MusicSource.Play();
        }

        if (SFXSource != null)
            SFXSource.playOnAwake = false;              
    }

    public void PlaySFX(AudioClip clip)
    {
        if (SFXSource != null)
            SFXSource.PlayOneShot(clip);
    }
}
