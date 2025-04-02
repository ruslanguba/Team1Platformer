using UnityEngine;

public class SpiritTrap : TrapBase
{
    //[SerializeField] private GameObject _greenEyes;
    //[SerializeField] private GameObject _redEyes;
    private SpiritSprite _sprite;
    [SerializeField] private float _patrolSpeed;
    [SerializeField] private float _activeSpeed;
    [SerializeField] private Vector2 _patrolAreaMin = new Vector2(-2, 0);
    [SerializeField] private Vector2 _patrolAreaMax = new Vector2(2, 1);
    //[SerializeField] private SpriteRenderer _spriteRenderer;
    //[SerializeField] private SpriteRenderer _eyeRenderer;
    private Collider2D _deathTrigger;
    private Vector2 _targetPoint;
    private Transform _stoneParent;
    private float _stoppingDistance = 0.1f;
    private float _currentSpeed;

    private void Start()
    {
        _sprite = GetComponentInChildren<SpiritSprite>();
        PickNewTarget();
        _stoneParent = transform.parent;
        _deathTrigger = _trapObject.GetComponent<Collider2D>();
        //_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _deathTrigger.enabled = false;
        _currentSpeed = _patrolSpeed;
        transform.parent = null;
    }

    private void Update()
    {
        MoveToTarget();
        FollowStone();
    }

    protected override void ActivateTrap(CharacterFire characterFire)
    {
        if(characterFire.IsBurning)
        {
            SetTriggerCharaceter();
            _currentSpeed = _activeSpeed;
            //_greenEyes.SetActive(false);
            //_redEyes.SetActive(true);
            _deathTrigger.enabled = true;
        }
    }

    protected override void DiactivateTrap()
    {
        base.DiactivateTrap();
        PickNewTarget();
        _currentSpeed = _patrolSpeed;
        //_greenEyes.SetActive(true);
        //_redEyes.SetActive(false);
        _deathTrigger.enabled = false;
    }

    private void SetTriggerCharaceter()
    {
        _targetPoint = _characterTransform.position;
        _sprite.CheckTarget(true, _targetPoint);
    }

    private void MoveToTarget()
    {
        _trapObject.position = Vector2.MoveTowards(_trapObject.position, _targetPoint, _currentSpeed * Time.deltaTime);
        if (Vector2.Distance(_trapObject.position, _targetPoint) < _stoppingDistance)
        {
            PickNewTarget();
        }
    }

    private void PickNewTarget()
    {
        Vector2 patrolCenter = transform.position;
        Vector2 offset = new Vector2(
            Random.Range(_patrolAreaMin.x, _patrolAreaMax.x),
            Mathf.Abs(Random.Range(_patrolAreaMin.y, _patrolAreaMax.y)));

        _targetPoint = patrolCenter + offset;
        _sprite.CheckTarget(false, _targetPoint);
    }

    private void FollowStone()
    {
        transform.position = _stoneParent.position;
    }

    //private void CheckDirection()
    //{
    //    _spriteRenderer.flipX = _trapObject.transform.position.x < _targetPoint.x;
    //}
}
