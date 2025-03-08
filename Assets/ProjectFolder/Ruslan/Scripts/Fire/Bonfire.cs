using System.Collections;
using UnityEngine;

public class Bonfire : FireBase
{
    [SerializeField] private float _burningTime = 30;
    private Coroutine _burningCoroutine;
    public override void HandleFire(bool isFireStarterBurning)
    {
        Debug.Log("Bonfire " + isFireStarterBurning);
        if (isFireStarterBurning)
        {
            if (_burningCoroutine != null)
            {
                StopCoroutine(_burningCoroutine);
            }
            _burningCoroutine = StartCoroutine(Burn());
        }
    }

    private IEnumerator Burn()
    {
        _isBurning = true;
        _fire.SetActive(true);
        yield return new WaitForSeconds(_burningTime);
        _isBurning = false;
        _fire.SetActive(false);
    }
}
