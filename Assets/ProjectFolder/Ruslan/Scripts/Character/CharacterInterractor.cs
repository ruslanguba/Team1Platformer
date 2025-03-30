using System;
using UnityEngine;

public class CharacterInterractor : MonoBehaviour
{
    public Action OnDisconnect;
    private Connector _connector;
    private PlayerInputHandler _input; // ссылка на класс обработки ввода
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
        _pressEHint.SetActive(false);
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
            OnDisconnect?.Invoke();
            return;
        }
        _currentInterractable?.OnInteract(this);       
    }

    public void ConnectToObject(Rigidbody2D rb)
    {
        _connector.ConnectToObject(rb);
    }
}
