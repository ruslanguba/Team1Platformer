using System.Collections;
using UnityEngine;

public class Flammable : FireBase
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _deleyTrigger;
    [SerializeField] private float _burnDuration;
    [SerializeField] private float _flameRadius;
    private Coroutine _coroutine;
  
    private void Start()
    {
        _fire = GetComponentInChildren<DeathTrigger>().gameObject;
        _spriteRenderer = _fire.GetComponent<SpriteRenderer>();
        _collider = _fire.GetComponent<Collider2D>();
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
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
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(_deleyTrigger);
        StartFire();
        _collider.enabled = true;
        yield return new WaitForSeconds(_burnDuration);
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        _isBurning = false;
        _coroutine = null;
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
