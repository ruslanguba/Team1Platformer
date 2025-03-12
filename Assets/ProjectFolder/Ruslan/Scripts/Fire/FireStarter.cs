using System;
using UnityEngine;

public class FireStarter : FireBase
{
    [SerializeField] float _fireStarterRadius;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Flammable>() != null)
        {
            collision.GetComponent<Flammable>().HandleFire(true);
            Debug.Log(collision.name);
        }
        if(collision.TryGetComponent(out Flammable flammable))
        {
            flammable.HandleFire(this);
            Debug.Log(flammable.name);
        }
    }
}
