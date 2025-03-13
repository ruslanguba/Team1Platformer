using UnityEngine;

public class RainDrop : MonoBehaviour
{
    [SerializeField] private RainDropSpawner _spawner;
    [SerializeField] private int _layer;

    public void SetSpawner(RainDropSpawner spawner)
    {
        _spawner = spawner;
        _layer = gameObject.layer;
        Physics2D.IgnoreLayerCollision(_layer, _layer);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IFireable fireable))
        {
            fireable.BraiseFire();
        }
        _spawner.SpawnRaindrop(this.transform);
    }
}
