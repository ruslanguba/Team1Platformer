using UnityEngine;

public class CharacterInterractor : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler _input; // ссылка на класс обработки ввода
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private Transform _interractPivot;
    [SerializeField] private float _checkRadius;
    [SerializeField] private HingeJoint2D _hingeJoint;
    [SerializeField] private LayerMask _ignoreLayerMask;
    private Connector _connector;

    private void Awake()
    {
        _input = GetComponent<PlayerInputHandler>();
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
        _connector = new Connector(_hingeJoint);
        _movement = GetComponent<CharacterMovement>();
    }

    public void Interact()
    {
        if(_connector._isConnected)
        {
            _connector.OnInterract();
            return;
        }
        var hit = Physics2D.OverlapCircle(_interractPivot.position, _checkRadius, ~_ignoreLayerMask);
        if (hit)
        {
            if (hit.gameObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.OnInteract(this);
            }
            //if (hit.gameObject.TryGetComponent(out IFireable fireable))
            //{
            //    GetComponent<CharacterFire>().BraiseFire();
            //    fireable.HandleFire(this);
            //}
        }
    }

    public void ConnectToObject(Rigidbody2D rb)
    {
        _connector.ConnectToObject(rb);
    }

    private void OnJointBreak2D(Joint2D brokenJoint)
    {
        _connector.DisconectObject();
    }

    private void FixedUpdate()
    {
        if(_connector._isConnected)
        {
            _connector.FixedUpdate();
            if (!_movement.IsGrounded())
            {
                _connector.DisconectObject();
            }
        }
    }
}
