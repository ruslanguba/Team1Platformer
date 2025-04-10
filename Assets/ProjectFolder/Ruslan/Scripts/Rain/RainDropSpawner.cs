using System.Collections;
using UnityEngine;

public class RainDropSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _raindropPrefab; // Префаб капли
    [SerializeField] private float _spawnWidth = 5f; // Ширина зоны спавна
    [SerializeField] private float _spawnRate = 0.5f; // Частота появления капель
    [SerializeField] private int _dropsCount = 20;
    [SerializeField] private Collider2D _ignoreCollider;

    private void Start()
    {
        StartCoroutine(StartRain());
    }

    private IEnumerator StartRain()
    {
        for (int i = 0; i < _dropsCount; i++)
        {
            CreateDrop();
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    public void SpawnRaindrop(Transform raindrop)
    {
        raindrop.GetComponent<Rigidbody2D>().linearVelocity = Vector3.zero;
        float spawnX = Random.Range(-_spawnWidth / 2, _spawnWidth / 2);
        Vector2 spawnPosition = new Vector2(transform.position.x + spawnX, transform.position.y);
        raindrop.position = spawnPosition;
    }

    private void CreateDrop()
    {
        GameObject raindrop = Instantiate(_raindropPrefab, transform.position, Quaternion.identity, transform);
        RainDrop rainDrop = raindrop.GetComponent<RainDrop>();
        rainDrop.SetSpawner(this);
        if(_ignoreCollider != null)
        {
            rainDrop.SetIgnoreCollider(_ignoreCollider);
        }
        SpawnRaindrop(raindrop.transform);
    }
}
