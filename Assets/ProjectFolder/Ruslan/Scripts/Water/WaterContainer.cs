using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterContainer : MonoBehaviour
{
    [SerializeField] private List<WaterParticle> _waterParticles;

    public Vector2 GetContainerSize()
    {
        float width = transform.localScale.x;
        float height = transform.localScale.y;
        return new Vector2(width, height);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out WaterParticle particle))
        {
            _waterParticles.Add(particle);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out WaterParticle particle))
        {
            _waterParticles.Remove(particle);
        }
    }
}
