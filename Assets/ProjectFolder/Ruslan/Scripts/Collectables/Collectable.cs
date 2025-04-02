using UnityEditor;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectable
{
    public void Collect()
    {

        Destroy(gameObject);
        // TODO Анимация подбора
    }
}
