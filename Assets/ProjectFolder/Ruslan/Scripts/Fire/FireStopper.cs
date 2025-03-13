using UnityEngine;

public class FireStopper : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.TryGetComponent(out FireBase fire))
        {
            fire.BraiseFire();
        }
    }
}
