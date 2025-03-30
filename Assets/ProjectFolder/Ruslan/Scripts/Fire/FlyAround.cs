using UnityEngine;

public class FlyAround : MonoBehaviour
{
    [SerializeField] private Transform _patrolCenter;  // Центр движения
    [SerializeField] private float _radius = 2f;       // Радиус, в котором выбирается случайная точка
    [SerializeField] private float _moveSpeed = 1f;    // Скорость движения
    [SerializeField] private float _randomTimeOffset = 0f; // Случайное смещение времени для каждого объекта

    private float _time; // Время для создания цикличности движения

    void Start()
    {
        // Генерация случайной точки в радиусе от центра для каждого объекта
        Vector2 randomDirection = Random.insideUnitCircle.normalized; // Генерация случайного направления
        Vector2 randomPosition = _patrolCenter.position + (Vector3)(randomDirection * _radius); // Умножаем на радиус и прибавляем к центру

        // Устанавливаем объект в случайную точку
        transform.position = randomPosition;

        // Случайное смещение времени для каждого объекта
        _randomTimeOffset = Random.Range(0f, Mathf.PI * 2f); // Генерация случайного смещения времени
    }

    void Update()
    {
        // Увеличиваем время с учетом смещения для каждого объекта
        _time += Time.deltaTime * _moveSpeed;

        // Добавляем случайное смещение времени
        float x = Mathf.Sin(_time + _randomTimeOffset) * _radius;  // Горизонтальная траектория
        float y = Mathf.Sin(_time * 0.5f + _randomTimeOffset) * _radius; // Вертикальная траектория

        // Перемещаем объект
        transform.position = new Vector2(_patrolCenter.position.x + x, _patrolCenter.position.y + y);
    }
}
