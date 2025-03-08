using UnityEngine;

public class Connector
{
    internal HingeJoint2D _hingeJoint;
    internal bool _isConnected;
    public Connector (HingeJoint2D hingeJoint)
    {
        _hingeJoint = hingeJoint;
        _hingeJoint.enabled = false;
        _isConnected = false;
    }

    internal void OnInterract()
    {
        if (_isConnected)
        {
            DisconectObject();
        }
    }

    internal void ConnectToObject(Rigidbody2D rb)
    {
        _hingeJoint.enabled = true;
        _hingeJoint.connectedBody = rb;
        _isConnected = true;
    }

    internal void DisconectObject()
    {
        _hingeJoint.connectedBody = null;
        _hingeJoint.enabled = false;
        _isConnected = false;
    }

    internal void FixedUpdate()
    {
        if (_isConnected)
        {
            Rigidbody2D rb = _hingeJoint.connectedBody;
            float torque = rb.inertia * rb.angularVelocity;
            if (torque > 100)
            {
                DisconectObject();
            }
        }
    }
}
