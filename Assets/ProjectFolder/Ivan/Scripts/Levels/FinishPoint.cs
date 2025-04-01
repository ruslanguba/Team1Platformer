using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private SaveData _saveData;
    [SerializeField] private bool _lastLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterFire characterFire = collision.GetComponent<CharacterFire>();

        if (characterFire != null && characterFire.IsBurning)
        {
            Debug.Log("Fin");
            _saveData.SaveAll();
            _saveData.UnlockNewLevel();
            if (_lastLevel)
            {
                _saveData.SaveFinish();
            }
            SceneController.instance.NextLevel();
        }
    }    
}
