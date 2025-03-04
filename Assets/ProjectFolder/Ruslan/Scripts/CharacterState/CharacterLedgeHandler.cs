using System;
using System.Collections;
using UnityEngine;

public class CharacterLedgeHandler : MonoBehaviour
{
    public event Action OnLedgeGrabbed;
    public event Action OnClimbFinished;

    private LedgeGrabDetector _ledgeDetector;
    [SerializeField] private float _climbDuration = 0.3f;

    private Rigidbody2D _rb;
    private bool _isGrabbingLedge;
    private bool _canDetectLedge = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ledgeDetector = GetComponent<LedgeGrabDetector>();
    }

    private void OnEnable()
    {
        _ledgeDetector.OnLedgeDetected += HandleLedgeDetected;
    }

    private void OnDisable()
    {
        _ledgeDetector.OnLedgeDetected -= HandleLedgeDetected;
    }

    private void HandleLedgeDetected()
    {
        if (!_canDetectLedge || _isGrabbingLedge) return;

        _isGrabbingLedge = true;
        _canDetectLedge = false;
        GrabLedge();
        OnLedgeGrabbed?.Invoke();
    }

    public void GrabLedge()
    {
        _rb.linearVelocity = Vector2.zero;
        _rb.gravityScale = 0;
    }

    public void ReleaseLedge()
    {
        _isGrabbingLedge = false;
        _rb.gravityScale = 1;
    }

    public void ClimbLedge()
    {
        StartCoroutine(ClimbRoutine());
    }

    private IEnumerator ClimbRoutine()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(1, 1.7f, 0);
        float elapsed = 0f;

        while (elapsed < _climbDuration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / _climbDuration);
            yield return null;
        }

        ReleaseLedge();

        OnClimbFinished?.Invoke();
        yield return new WaitForSeconds(0.1f);
        _canDetectLedge = true;
    }

    public void DisableLedgeDetection(float delay = 0.2f)
    {
        StartCoroutine(EnableLedgeDetectionAfterDelay(delay));
    }

    private IEnumerator EnableLedgeDetectionAfterDelay(float delay)
    {
        _canDetectLedge = false;
        yield return new WaitForSeconds(delay);
        _canDetectLedge = true;
    }
}
