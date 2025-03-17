using System;
using UnityEngine;

public class CharacterCollect : MonoBehaviour
{
    public event Action OnCollect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collect();
            OnCollect?.Invoke();
        }
    }
}
