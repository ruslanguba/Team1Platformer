using System;
using UnityEngine;

public class LedgeGrabDetector : MonoBehaviour
{
    public event Action OnLedgeDetected;

    [SerializeField] private Transform _upPoint;
    [SerializeField] private Transform _downPoint;  
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private float _checkDistance = 0.2f;

    private void FixedUpdate()
    {
        if (CheckForLedge())
        {
            OnLedgeDetected?.Invoke();
        }
    }

    private bool CheckForLedge()
    {
        bool lowHit = Physics2D.Raycast(_downPoint.position, Vector2.right, _checkDistance, _wallLayer);
        bool highHit = Physics2D.Raycast(_upPoint.position, Vector2.right, _checkDistance, _wallLayer);
        return lowHit && !highHit;
    }
}
