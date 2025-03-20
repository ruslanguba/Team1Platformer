using UnityEngine;

public class WaterEmitter : MonoBehaviour
{
    [SerializeField] private GameObject _waterParticlePrefab;
    [SerializeField] private float _spawnRate = 0.1f; // „астота по€влени€ капель
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _pariclesCount;

    private void SpawnWaterParticle()
    {
        GameObject wp = Instantiate(_waterParticlePrefab, _spawnPoint.position, Quaternion.identity);
        wp.GetComponent<Rigidbody2D>().AddForce(Vector2.one * -1);
        _pariclesCount++;
    }

    public void FillContainer(Vector2 containerSize)
    {
        int count = CalculateWaterParticles(containerSize);
        _pariclesCount = count;
        for (int i = 0; i < count; i++)
        {
            SpawnWaterParticle();
        }
    }

    private int CalculateWaterParticles(Vector2 containerSize)
    {
        float particleRadius = _waterParticlePrefab.transform.localScale.x / 2;
        float containerArea = containerSize.x * containerSize.y;
        float particleArea = Mathf.PI * Mathf.Pow(particleRadius, 2);

        //  оличество капель, которые могут поместитьс€ в водоеме
        return Mathf.FloorToInt(containerArea / particleArea);
    }
    
}
