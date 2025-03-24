using System;
using UnityEngine;

public class CharacterInterractor : MonoBehaviour
{
    public Action<Transform> OnConnect;
    public Action OnDisconnect;
    //[SerializeField] private Transform _interractPivot;
    //[SerializeField] private float _checkRadius;
    [SerializeField] private float _breakingTorque = 200;
    //[SerializeField] private LayerMask _ignoreLayerMask;
    //[SerializeField] private float _kostilMoveBlockForce;
    private HingeJoint2D _hingeJoint;
    internal Connector _connector;
    private PlayerInputHandler _input; // ссылка на класс обработки ввода
    private CharacterMovementHandler _movement;
    [SerializeField] private IInteractable _currentInterractable;
    [SerializeField] private GameObject _pressEHint;

    private void Awake()
    {
        _input = GetComponent<PlayerInputHandler>();
        GetComponentInChildren<InteractorDetector>().Initialize(this);
    }

    private void OnEnable()
    {
        _input.OnUseInput += Interact;
    }

    private void OnDisable()
    {
        _input.OnUseInput -= Interact;
    }

    void Start()
    {
        _hingeJoint = GetComponent<HingeJoint2D>();
        _connector = new Connector(_hingeJoint, _breakingTorque);
        _movement = GetComponent<CharacterMovementHandler>();
    }

    public void SetInteractable(IInteractable interactable)
    {
        _pressEHint.SetActive(true);
        _currentInterractable = interactable;
    }
    public void ClearInteractable()
    {
        _pressEHint.SetActive(false);
        _currentInterractable = null;
    }

    public void Interact()
    {
        if (_connector._isConnected)
        {
            _connector.DisconectObject();
            OnDisconnect?.Invoke();
            return;
        }
        _currentInterractable?.OnInteract(this);
        

        //if(_connector._isConnected)
        //{
        //    _connector.OnInterract();
        //    return;
        //}
        //var hit = Physics2D.OverlapCircle(_interractPivot.position, _checkRadius, ~_ignoreLayerMask);
        //if (hit)
        //{
        //    if (hit.gameObject.TryGetComponent(out IInteractable interactable))
        //    {
        //        interactable.OnInteract(this);
        //    }
        //    if (!_movement.IsGrounded() && hit.gameObject.TryGetComponent(out Interactable movable))
        //    {
        //        Vector2 force = new Vector2(_movement.GetFacingDirection() * _kostilMoveBlockForce, 0);
        //        movable.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        //        Debug.Log(movable.name);
        //    }
        //}
    }

    public void ConnectToObject(Rigidbody2D rb)
    {
        _connector.ConnectToObject(rb);
        Debug.Log("ConnectToObject");
        OnConnect?.Invoke(rb.transform);
    }

    private void OnJointBreak2D(Joint2D brokenJoint)
    {
        _connector.DisconectObject();
        OnDisconnect?.Invoke();
    }

    private void FixedUpdate()
    {
        if(_connector._isConnected)
        {
            _connector.FixedUpdate();
            if (!_movement.IsGrounded())
            {
                _connector.DisconectObject();
                OnDisconnect?.Invoke();
            }
        }
    }
}
