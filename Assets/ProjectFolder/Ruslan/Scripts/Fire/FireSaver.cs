using System;
using UnityEngine;

public class FireSaver : FireBase
{
    public override void HandleFire(bool isFireStarterBurning)
    {
        _fire.SetActive(isFireStarterBurning);
    }
}
