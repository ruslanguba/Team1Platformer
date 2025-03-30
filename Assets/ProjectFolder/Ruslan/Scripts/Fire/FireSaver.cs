using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireSaver : FireBase
{
    public override void HandleFire(bool isFireStarterBurning)
    {
        if(isFireStarterBurning)
        {
            _fire.SetActive(true);
            _isBurning = true;
        }
    }
}
