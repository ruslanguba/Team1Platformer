using UnityEngine;

public class CompleatLevel : MonoBehaviour
{
    [SerializeField] GameObject _scorePanel;
    [SerializeField] GameObject _block;
    private bool _isMovingToFinish;
    private CharacterMovementHandler _moveable;
    private CharacterRespown _characterRespown;
    private CharacterFire _characterFire;
    private ConditionsHint _conditionsHint;
    private int _activeBonfires = 0;
    private int _requiredBonfires = 3;
    private Animator _animator;

    private void Awake()
    {
        _characterRespown = FindFirstObjectByType<CharacterRespown>();     
    }

    private void OnEnable()
    {
        _characterRespown.OnRespownPoindFound += AddActiveBonfire;
    }

    private void OnDisable()
    {
        _characterRespown.OnRespownPoindFound -= AddActiveBonfire;
    }

    private void Start()
    {
        _characterFire = _characterRespown.GetComponent<CharacterFire>();
        _animator = GetComponentInChildren<Animator>();
        _conditionsHint = GetComponentInChildren<ConditionsHint>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out CharacterMoveController controller) && IsCanCompleteLvl())
        {
            TakeControl(controller);
            _animator.SetTrigger("go");
        }
        else
        {
            SetHintText();
        }
    }

    private void Update()
    {
        MoveToFinish();
    }

    private bool IsCanCompleteLvl()
    {
        return _activeBonfires >= _requiredBonfires && _characterFire.IsBurning;
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

    private void AddActiveBonfire(Vector2 _)
    {
        _activeBonfires++;
    }

    private void SetHintText()
    {
        if(IsCanCompleteLvl())
        {
            _conditionsHint.gameObject.SetActive(false);
        }
        else
        {
            _conditionsHint.gameObject.SetActive(true);
            _conditionsHint.SetHintText(_activeBonfires < _requiredBonfires);
        }
    }
}
