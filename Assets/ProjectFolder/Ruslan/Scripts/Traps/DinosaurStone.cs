using Unity.VisualScripting;
using UnityEngine;

public class DinosaurStone : TrapBase
{
    private Collider2D _triggerCollider;
    private DinosaurTrap _dinosaurTrap;
    private Rigidbody2D _stoneInTrapRigidbody;
    private Transform _stoneInTrapTransform;

    private void Start()
    {
        _dinosaurTrap = GetComponentInParent<DinosaurTrap>();
        _triggerCollider = GetComponent<Collider2D>();
    }

    protected override void HandleTriggerEnter(Collider2D collision)
    {
        if (collision.TryGetComponent(out Interactable movable))
        {
            _stoneInTrapRigidbody = movable.GetComponent<Rigidbody2D>();
            _stoneInTrapTransform = movable.GetComponent<Transform>();
            movable.enabled = false;
            _dinosaurTrap.CloseJawsAction(true);
            SetStonePosition();
        }
    }

    private void SetStonePosition()
    {
        _stoneInTrapRigidbody.bodyType = RigidbodyType2D.Static;
        _stoneInTrapTransform.position = transform.position;
        _dinosaurTrap.SetCollidersEnabled(false);
        _triggerCollider.enabled = false;
    }
}
