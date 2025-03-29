using UnityEngine;

public class SFXManager : MonoBehaviour
{ 
    public AudioSource SFXSource;

    private void Start()
    {
        if (SFXSource != null)
            SFXSource.playOnAwake = false;              
    }

    public void PlaySFX(AudioClip clip)
    {
        if (SFXSource != null)
            SFXSource.PlayOneShot(clip);
    }
}
