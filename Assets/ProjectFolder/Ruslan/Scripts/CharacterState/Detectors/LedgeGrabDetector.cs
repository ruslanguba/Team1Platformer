using System;
using System.Collections;
using UnityEngine;

public class LedgeGrabDetector : MonoBehaviour
{
    public event Action OnLedgeDetected;

    [SerializeField] private Transform _upPoint;
    [SerializeField] private Transform _downPoint;
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private float _checkRadius = 0.2f;
    [SerializeField] private float _stopCheckingTime = 0.2f;

    private bool _canDetectLedge = true;

    private void FixedUpdate()
    {
        if (!_canDetectLedge) return;

        if (CheckForLedge())
        {
            OnLedgeDetected?.Invoke();
            DisableLedgeDetection();
        }
    }

    private bool CheckForLedge()
    {
        bool lowHit = Physics2D.OverlapCircle(_downPoint.position, _checkRadius, _wallLayer);
        bool highHit = Physics2D.OverlapCircle(_upPoint.position, _checkRadius, _wallLayer);
        return lowHit && !highHit;

        //bool lowHit = Physics2D.Raycast(_downPoint.position, Vector2.right, _checkDistance, _wallLayer);
        //bool highHit = Physics2D.Raycast(_upPoint.position, Vector2.right, _checkDistance, _wallLayer);
        //return lowHit && !highHit;
    }

    private void EnableLedgeDetection()
    {
        _canDetectLedge = true;
    }
    private void DisableLedgeDetection()
    {
        _canDetectLedge = false;
    }

    public void PauseDetection()
    {
        StartCoroutine(EnableLedgeDetectionWithDelayForSeconds());
    }

    private IEnumerator EnableLedgeDetectionWithDelayForSeconds()
    {
        DisableLedgeDetection();
        yield return new WaitForSeconds(_stopCheckingTime);
        EnableLedgeDetection();
    }
}
