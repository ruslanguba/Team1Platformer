using UnityEngine;

public class RainDrop : MonoBehaviour
{
    private RainDropSpawner _spawner;
    private int _layer;
    private Collider2D _ignoreCollider;

    public void SetSpawner(RainDropSpawner spawner)
    {
        _spawner = spawner;
        _layer = gameObject.layer;
        Physics2D.IgnoreLayerCollision(_layer, _layer);
    }

    public void SetIgnoreCollider(Collider2D ignoreCollider)
    {
        _ignoreCollider = ignoreCollider;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IFireable fireable))
        {
            fireable.BraiseFire();
        }
        if (_ignoreCollider == null || collision != _ignoreCollider)
        {
            _spawner.SpawnRaindrop(this.transform);
        }
    }
}
