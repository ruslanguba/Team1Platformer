using UnityEngine;

public class FireBase : MonoBehaviour, IFireable
{
    [SerializeField] protected GameObject _fire;
    protected bool _isBurning = false;
    public bool IsBurning => _isBurning;

    public virtual void HandleFire(bool isFireStarterBurning) { }

    //public void OnInteract(CharacterInterractor interactor) 
    //{
    //    _isBurning = interactor.GetComponent<CharacterFire>().IsBurning;
    //    HandleFire(_isBurning);
    //}

    public void BraiseFire()
    {
        if (_isBurning)
        {
            Debug.Log("BraiseFire");
            _isBurning = false;
            _fire.SetActive(false);
        }
    }
}
