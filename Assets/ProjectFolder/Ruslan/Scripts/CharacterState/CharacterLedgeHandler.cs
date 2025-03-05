using System;
using System.Collections;
using UnityEngine;

public class CharacterLedgeHandler : MonoBehaviour
{
    public event Action OnClimbFinished;

    private LedgeGrabDetector _ledgeDetector;
    [SerializeField] private float _climbDuration = 0.3f;

    private Rigidbody2D _rb;
    private bool _isGrabbingLedge;

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
        _isGrabbingLedge = true;
    }

    public void GrabLedge()
    {
        Debug.Log("Character Ledge Handler GrabLedge");
        _rb.linearVelocity = Vector2.zero;
        _rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void ReleaseLedge()
    {
        _isGrabbingLedge = false;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _ledgeDetector.PauseDetection();
    }

    public void ClimbLedge()
    {
        StartCoroutine(ClimbRoutine());
    }

    private IEnumerator ClimbRoutine()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(1 * transform.localScale.x, 1.7f, 0);
        float elapsed = 0f;

        while (elapsed < _climbDuration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / _climbDuration);
            yield return null;
        }

        ReleaseLedge();

        OnClimbFinished?.Invoke();
    }

    public bool IsGrabbingLedge()
    {
        return _isGrabbingLedge;
    }
}
