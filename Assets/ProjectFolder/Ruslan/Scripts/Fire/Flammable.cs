using System.Collections;
using UnityEngine;

public class Flammable : FireBase
{
    [SerializeField] private float _fireSpreadDelay;
    [SerializeField] private float _burnDuration;
    [SerializeField] private float _flameRadius;
    [SerializeField] private GameObject _animatedFire; 
    //private SpriteRenderer _spriteRenderer;
    private Collider2D _deathCollider;
    private Coroutine _coroutine;
  
    private void Start()
    {
        _fire = GetComponentInChildren<FireDeathTrigger>().gameObject;
        //_spriteRenderer = _fire.GetComponent<SpriteRenderer>();
        _deathCollider = _fire.GetComponent<Collider2D>();
        //_spriteRenderer.enabled = false;
        _deathCollider.enabled = false;
        _animatedFire.SetActive(false);
    }
    public override void HandleFire(bool isFireStarterBurning)
    {
        if (isFireStarterBurning)
        {
            if(_coroutine == null)
            {
                _coroutine = StartCoroutine(BurnWithDeley(isFireStarterBurning));
            }
        }
    }

    IEnumerator BurnWithDeley(bool isFireStarterBurning)
    {
        _isBurning = isFireStarterBurning;
        //_spriteRenderer.enabled = true;
        _deathCollider.enabled = true;
        _animatedFire.SetActive(true);
        yield return new WaitForSeconds(_fireSpreadDelay);
        StartFire();
        yield return new WaitForSeconds(_burnDuration);
        //_spriteRenderer.enabled = false;
        _deathCollider.enabled = false;
        _isBurning = false;
        _coroutine = null;
        _animatedFire.SetActive(false);
    }

    private void StartFire()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _flameRadius);
        foreach (Collider2D collider in colliders)
        {
            if(collider.TryGetComponent(out Flammable flammable))
            {
                flammable.HandleFire(true);
            }
        }
    }
}
