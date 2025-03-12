using System;
using UnityEngine;

public class CharacterRespown : MonoBehaviour
{
    public event Action<Vector2> OnRespownPoindFound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out RespawnPoint respawnPoint) )
        {
            OnRespownPoindFound?.Invoke(respawnPoint.transform.position);
        }
    }
}
