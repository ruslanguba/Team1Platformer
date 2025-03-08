using System.Collections;
using UnityEngine;

public class Flammable : FireBase
{
    [SerializeField] private RespawnTrigger _trigger;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _deleyTrigger;
    [SerializeField] private float _burnDuration;
  
    private void Start()
    {
        _fire = GetComponentInChildren<RespawnTrigger>().gameObject;
        _spriteRenderer = _fire.GetComponent<SpriteRenderer>();
        _collider = _fire.GetComponent<Collider2D>();
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
    }
    public override void HandleFire(bool isFireStarterBurning)
    {
        if (isFireStarterBurning)
        {
            StartCoroutine(BurnWithDeley(isFireStarterBurning));
        }
    }

    IEnumerator BurnWithDeley(bool isFireStarterBurning)
    {
        _isBurning = isFireStarterBurning;
        _spriteRenderer.enabled = true;
        yield return new WaitForSeconds(_deleyTrigger);
        _collider.enabled = true;
        yield return new WaitForSeconds(_burnDuration);
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        _isBurning = false;
    }
}
