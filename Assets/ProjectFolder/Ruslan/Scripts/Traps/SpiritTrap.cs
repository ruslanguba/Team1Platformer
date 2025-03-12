using UnityEngine;

public class SpiritTrap : TrapBase
{
    [SerializeField] private Collider2D _respawnTrigger;
    [SerializeField] private SpriteRenderer[] _sprites;
    [SerializeField] private Color _spritesStartColor;
    [SerializeField] private float _patrolSpeed;
    [SerializeField] private float _activeSpeed;
    [SerializeField] private Vector2 _patrolAreaMin = new Vector2(-2, 0);
    [SerializeField] private Vector2 _patrolAreaMax = new Vector2(2, 1);
    private Vector3 _targetPoint;
    private float _stoppingDistance = 0.1f;
    private float _currentSpeed;

    private void Start()
    {
        PickNewTarget();
        _spritesStartColor = _sprites[0].color;
        _respawnTrigger = _trapObject.GetComponent<Collider2D>();
        _respawnTrigger.enabled = false;
    }

    private void Update()
    {
        MoveToTarget();
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
            _respawnTrigger.enabled = true;
        }
    }

    protected override void DiactivateTrap()
    {
        base.DiactivateTrap();PickNewTarget();
        foreach (var sprite in _sprites)
        {
            sprite.color = _spritesStartColor;
        }
        _respawnTrigger.enabled = false;
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
        // Выбираем случайную точку в локальных координатах
        Vector2 localTarget = new Vector2(
            Random.Range(_patrolAreaMin.x, _patrolAreaMax.x),
            Random.Range(_patrolAreaMin.y, _patrolAreaMax.y)
        );

        // Конвертируем в мировые координаты
        _targetPoint = _trapObject.parent.TransformPoint(localTarget);
        _currentSpeed = _patrolSpeed;
    }
}
