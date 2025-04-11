using UnityEditor;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectable
{
    public void Collect()
    {
        GetComponent<Collider2D>().enabled = false;
        // TODO Анимация подбора
    }

    public Transform GetCollectableTransform()
    {
        return transform;
    }
}
