using UnityEngine;

public class FireBase : MonoBehaviour, IFireable
{
    [SerializeField] protected GameObject _fire;
    protected bool _isBurning = false;
    public bool IsBurning => _isBurning;

    public virtual void HandleFire(bool isFireStarterBurning) { }
}
