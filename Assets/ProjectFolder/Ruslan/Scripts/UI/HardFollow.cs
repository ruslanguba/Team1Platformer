using UnityEngine;

public class HardFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    
    private void FixedUpdate()
    {
        transform.position = _target.position;
        transform.rotation = Quaternion.Euler(Vector3.zero);

    }
}
