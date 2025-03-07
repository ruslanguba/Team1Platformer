using UnityEngine;

public class FireStopper : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.TryGetComponent(out CharacterFire characterFire))
        {
            characterFire.BraiseFire();
        }
    }
}
