using System;
using UnityEngine;

public class CharacterRespown : MonoBehaviour
{
    public event Action<Vector2> OnRespownPoindFound;
    private CharacterFire _characterFire;

    private void Start()
    {
        _characterFire = GetComponent<CharacterFire>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out RespawnPoint respawnPoint) && _characterFire.IsBurning)
        {
            OnRespownPoindFound?.Invoke(respawnPoint.transform.position);
        }
    }
}
