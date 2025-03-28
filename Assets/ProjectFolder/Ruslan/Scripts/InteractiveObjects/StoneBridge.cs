using System.Collections;
using UnityEngine;

public class StoneBridge : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _isFallRight;
    private Rigidbody2D _rb;
    private HingeJoint2D _joint;
    private Collider2D _activatorTrigger;
    private float _triggerPositionOffset;
    private float _maxAngleLimmit;

    private void Start()
    {
        _activatorTrigger = GetComponent<Collider2D>();
        _joint = GetComponent<HingeJoint2D>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Kinematic;
        SetFallDirection();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterFire character))
        {
            OnTriggerEnterHandler(character);
        }
    }

    public void OnInteract(CharacterInterractor interactor)
    {
        if (_isFallRight)
        {
            _rb.AddForce(Vector2.right, ForceMode2D.Impulse);
        }
        else
        {
            _rb.AddForce(Vector2.left, ForceMode2D.Impulse);
        }
    }

    protected virtual void OnTriggerEnterHandler(CharacterFire character)
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        StartCheckRotation();
    }

    protected virtual void StartCheckRotation()
    {
        StartCoroutine(CheckRotation());
    }

    private void SetFallDirection()
    {
        Vector2 anchorPosition;

        if (_isFallRight)
        {
            anchorPosition = new Vector2(0.5f, 0);
            _triggerPositionOffset = -1;
            _maxAngleLimmit = 90;
        }
        else
        {
            anchorPosition = new Vector2(-0.5f, 0);
            _triggerPositionOffset = 1;
            _maxAngleLimmit = -90;
        }
        _joint.anchor = anchorPosition;
        _activatorTrigger.offset = new Vector2(_triggerPositionOffset, _activatorTrigger.offset.y);
        JointAngleLimits2D limits = _joint.limits;
        limits.max = _maxAngleLimmit;
        _joint.limits = limits;
        _joint.useLimits = true;
    }

    private IEnumerator CheckRotation()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (Mathf.Abs(transform.rotation.z) > 0.5)
            {
                _activatorTrigger.enabled = false;
                yield break;
            }
        }
    }
}
