using UnityEngine;

public class PlatformParenter : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Platform>() != null)
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Platform>() != null)
        {
            transform.SetParent(null);
        }
    }
}
