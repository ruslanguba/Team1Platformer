using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private Rigidbody2D _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void OnInteract(CharacterInterractor interractor)
    {
        interractor.ConnectToObject(_rb);
        
    }
}
