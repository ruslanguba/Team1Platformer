using UnityEngine;

public class FireBridgeTree : StoneBridge
{
    [SerializeField] private GameObject _flamable;

    protected override void OnTriggerEnterHandler(CharacterFire character)
    {
        if (!character.IsBurning)
        {
            base.OnTriggerEnterHandler(character);
            _flamable.SetActive(false);
        }
    }

}
