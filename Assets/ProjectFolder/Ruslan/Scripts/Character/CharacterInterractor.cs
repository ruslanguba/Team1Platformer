using System;
using UnityEngine;

public class CharacterInterractor : MonoBehaviour
{
    public Action<Transform> OnConnect;
    public Action OnDisconnect;
    //[SerializeField] private float _breakingTorque = 200;
    //[SerializeField] private HingeJoint2D _hingeJoint;
    private Connector _connector;
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
        _connector = GetComponent<Connector>();
        //_connector = new Connector(_hingeJoint, _breakingTorque);
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
        if (_connector.IsConnected)
        {
            _connector.DisconectObject();
            Debug.Log("Disconnect From Object");
            OnDisconnect?.Invoke();
            return;
        }
        _currentInterractable?.OnInteract(this);       
    }

    public void ConnectToObject(Rigidbody2D rb)
    {
        _connector.ConnectToObject(rb);
        Debug.Log("Connect To Object");
        OnConnect?.Invoke(rb.transform);
    }

    private void OnJointBreak2D(Joint2D brokenJoint)
    {
        OnDisconnect?.Invoke();
        Debug.Log("Disconnect From Object");
        _connector.DisconectObject();
    }

    //private void FixedUpdate()
    //{
    //    if(_connector._isConnected)
    //    {
    //        _connector.FixedUpdate();
    //        if (!_movement.IsGrounded())
    //        {
    //            _connector.DisconectObject();
    //            OnDisconnect?.Invoke();
    //        }
    //    }
    //}
}
