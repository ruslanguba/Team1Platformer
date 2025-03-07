using System;
using System.Collections;
using UnityEngine;

public class LedgeGrabDetector : MonoBehaviour
{
    public event Action OnLedgeDetected;
    public event Action OnLedgeLost;

    [SerializeField] private Transform _upPoint;
    [SerializeField] private Transform _downPoint;
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private float _checkRadius = 0.2f;
    [SerializeField] private float _stopCheckingTime = 1;

    private bool _canDetectLedge = true;

    public bool CheckForLedge()
    {
        if (!_canDetectLedge)
        {
            return false;
        }
        else
        {
            bool lowHit = Physics2D.OverlapCircle(_downPoint.position, _checkRadius, _wallLayer);
            bool highHit = Physics2D.OverlapCircle(_upPoint.position, _checkRadius, _wallLayer);
            return lowHit && !highHit;
        }
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
