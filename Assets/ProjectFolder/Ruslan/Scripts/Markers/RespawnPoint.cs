using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public bool IsFound { get; private set; } = false;

    public void SaveRespownPoint()
    {
        IsFound = true;
    }
}
