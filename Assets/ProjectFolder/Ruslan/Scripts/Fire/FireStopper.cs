using UnityEngine;

public class FireStopper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IFireable fireable))
        {
            fireable.BraiseFire();
        }
    }
}
