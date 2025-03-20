using UnityEngine;

public class parallax : MonoBehaviour
{
    public GameObject cam;

    public float Parallax;
    float startPosX;

     void Start()
    {
        startPosX = transform.position.x;
    }

     void Update()
    {
        float distX = (cam.transform.position.x * (1 - Parallax));
        transform.position = new Vector3(startPosX + distX, transform.position.y, transform.position.z);
    }
}
