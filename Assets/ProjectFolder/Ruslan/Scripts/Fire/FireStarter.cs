using System;
using UnityEngine;

public class FireStarter : MonoBehaviour
{
    public event Action SaveGame;
    private bool _isBurning = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IFireable>() != null )
        {
            HandleFire(collision.GetComponent<IFireable>());
        }
    }

    private void HandleFire(IFireable fire)
    {
        if (fire is FireSaver)
        {
            SaveGame?.Invoke();
        }

        fire.HandleFire(_isBurning);
    }
}
