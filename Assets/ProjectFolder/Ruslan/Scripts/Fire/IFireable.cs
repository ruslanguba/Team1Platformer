using UnityEngine;

public interface IFireable
{
    public bool IsBurning { get; }
    void HandleFire(bool isBurning);
    void BraiseFire();
}
