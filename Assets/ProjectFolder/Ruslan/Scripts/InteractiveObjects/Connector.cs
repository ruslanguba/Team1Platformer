using UnityEngine;

public class Connector
{
    internal HingeJoint2D _hingeJoint;
    internal bool _isConnected;
    internal float _breakingTorque;
    public Connector (HingeJoint2D hingeJoint, float breakingTorque)
    {
        _hingeJoint = hingeJoint;
        _hingeJoint.enabled = false;
        _isConnected = false;
        _breakingTorque = breakingTorque;
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
        ////Забираем массу у толкаемого обьекта
        //rb.mass /= 4;
    }

    internal void DisconectObject()
    {
        ////Возвращаем массу толкаемому обьекта
        //_hingeJoint.connectedBody.mass *= 4;

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
            Debug.Log(torque);
            if (Mathf.Abs(torque) > _breakingTorque)
            {
                DisconectObject();
            }
        }
    }
}
