using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<CharacterMovement>() != null)
        {
            collision.transform.position = _spawnPoint.position;
        }
    }
}
