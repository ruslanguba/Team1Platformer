using UnityEngine;

public class SpiritTrap : TrapBase
{
    [SerializeField] private SpriteRenderer[] _sprites;
    [SerializeField] private float _patrolSpeed;
    [SerializeField] private float _activeSpeed;
    [SerializeField] private Vector2 _patrolAreaMin = new Vector2(-2, 0);
    [SerializeField] private Vector2 _patrolAreaMax = new Vector2(2, 1);
    private Color _spritesStartColor;
    private Collider2D _deathTrigger;
    private Vector3 _targetPoint;
    private Transform _stoneParent;
    private float _stoppingDistance = 0.1f;
    private float _currentSpeed;

    private void Start()
    {
        PickNewTarget();
        _stoneParent = transform.parent;
        _spritesStartColor = _sprites[0].color;
        _deathTrigger = _trapObject.GetComponent<Collider2D>();
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
            foreach(var sprite in _sprites)
            {
                sprite.color = Color.red;
            }
            _deathTrigger.enabled = true;
        }
    }

    protected override void DiactivateTrap()
    {
        base.DiactivateTrap();
        PickNewTarget();
        _currentSpeed = _patrolSpeed;
        foreach (var sprite in _sprites)
        {
            sprite.color = _spritesStartColor;
        }
        _deathTrigger.enabled = false;
    }

    private void SetTriggerCharaceter()
    {
        _targetPoint = _characterTransform.position;
    }

    private void MoveToTarget()
    {
        _trapObject.position = Vector3.MoveTowards(_trapObject.position, _targetPoint, _currentSpeed * Time.deltaTime);

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
    }

    private void FollowStone()
    {
        transform.position = _stoneParent.position;
    }
}
