using System;
using UnityEngine;

public class CharacterDeath : MonoBehaviour
{
    public Action OnDeathTriggerEntered;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<DeathTrigger>() != null)
        {
            OnDeathTriggerEntered?.Invoke();
        }
    }
}
