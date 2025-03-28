using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _heartTransform;
    [SerializeField] private Transform _followTarget;
    [SerializeField] private float _heartSmoothTime = 0.3f;

    private Vector3 _heartVelocity = Vector3.zero;

    public float radius = 0.1f; // Радиус в пределах которого выбираются точки
    public float _targetSmoothTime = 0.2f; // Время сглаживания
    public float _pointStayTime = 1f; // Время ожидания перед сменой точки

    private Vector3 _targetPoint; // Текущая цель
    private Vector3 _targetVelocity = Vector3.zero; // Вектор скорости
    private float _waitTime; // Время ожидания перед сменой точки

    private void Start()
    {
        _heartTransform.parent = null;

        // Выбираем стартовую цель
        PickNewTarget();
    }

    void FixedUpdate()
    {
        MoveHeart();
        MoveTarget();
    }

    private void MoveHeart()
    {
        if (_target == null) return;
        _heartTransform.position = Vector3.SmoothDamp(_heartTransform.position, _target.position, ref _heartVelocity, _heartSmoothTime);
    }

    private void MoveTarget()
    {
        if (_followTarget == null) return; // Защита от ошибки

        _target.localPosition = Vector3.SmoothDamp(_target.localPosition, _targetPoint, ref _targetVelocity, _targetSmoothTime);

        // Если почти достигли цели, ждем перед выбором новой точки
        if (Vector3.Distance(_target.localPosition, _targetPoint) < 0.05f) // Уменьшил порог
        {
            _waitTime -= Time.deltaTime;
            if (_waitTime <= 0)
            {
                PickNewTarget();
            }
        }
    }

    void PickNewTarget()
    {
        if (_followTarget == null) return;

        // Выбираем случайную точку ВОКРУГ _followTarget
        Vector2 randomOffset = Random.insideUnitCircle * radius;
        _targetPoint = _followTarget.localPosition + new Vector3(randomOffset.x, randomOffset.y, 0);

        _waitTime = _pointStayTime; // Устанавливаем таймер ожидания
    }
}
