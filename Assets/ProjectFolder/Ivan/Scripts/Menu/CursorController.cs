using UnityEngine;

public class CursorController : MonoBehaviour
{
    private Camera _camera;
    void Start()
    {
        Cursor.visible = false;
        _camera = Camera.main;
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 realPos = _camera.ScreenToWorldPoint(mousePos);
        transform.position = realPos;
    }
}
