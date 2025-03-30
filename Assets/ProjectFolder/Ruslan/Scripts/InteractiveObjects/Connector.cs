using System;
using UnityEngine;

public class Connector: MonoBehaviour
{
    public Action<Transform> OnConnect;
    public Action<Transform> OnDisconnect;

    [SerializeField] private float _breakingTorque = 200;
    private CharacterMovementHandler _movementHandler;
    private Rigidbody2D _conectedBody;
    private HingeJoint2D _hingeJoint;
    private bool _isConnected;

    public bool IsConnected => _isConnected;

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint2D>();
    }

    private void Start()
    {
        _isConnected = false;
        _movementHandler = GetComponent<CharacterMovementHandler>();
        if(_hingeJoint != null)
        {
            _hingeJoint.enabled = false;
        }
    }

    private void OnJointBreak2D(Joint2D brokenJoint)
    {
        DisconectObject();
    }

    public void OnInterract()
    {
        if (_isConnected)
        {
            DisconectObject();
        }
    }

    public void ConnectToObject(Rigidbody2D rb)
    {
        OnConnect?.Invoke(rb.transform);
        _conectedBody = rb;
        _hingeJoint.enabled = true;
        _hingeJoint.connectedBody = _conectedBody;
        _isConnected = true;
    }

    public void DisconectObject()
    {
        if(_conectedBody != null)
        {
            OnDisconnect?.Invoke(_conectedBody.transform);
        }
        _hingeJoint.connectedBody = null;
        _hingeJoint.enabled = false;
        _isConnected = false;   
        _conectedBody = null;
    }

    private void FixedUpdate()
    {
        if (_isConnected)
        {
            float torque = _conectedBody.inertia * _conectedBody.angularVelocity;
            if (Mathf.Abs(torque) > _breakingTorque || !_movementHandler.IsGrounded())
            {
                DisconectObject();
            }
        }
    }
}
