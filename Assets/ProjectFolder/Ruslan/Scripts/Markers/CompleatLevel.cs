using UnityEngine;

public class CompleatLevel : MonoBehaviour
{
    [SerializeField] GameObject _scorePanel;
    private bool _isMovingToFinish;
    private CharacterMovementHandler _moveable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out CharacterMoveController controller))
        {
            TakeControl(controller);
        }
    }

    private void Update()
    {
        MoveToFinish();
    }

    private void MoveToFinish()
    {
        if (_isMovingToFinish)
        {
            _moveable.Move(Vector2.right);
        }
    }

    private void TakeControl(CharacterMoveController controller)
    {
        controller.enabled = false;
        _isMovingToFinish = true;
        _moveable = controller.gameObject.GetComponent<CharacterMovementHandler>();
        _scorePanel.SetActive(true);
    }
}
