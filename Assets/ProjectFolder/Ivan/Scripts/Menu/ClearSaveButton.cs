using UnityEngine;
using UnityEngine.UI;

public class ClearSaveButton : MonoBehaviour
{
    [SerializeField] private Button _clearButton;
    [SerializeField] private LevelManager _levelManager;

    void Start()
    {
        // Назначаем метод на событие нажатия кнопки
        _clearButton.onClick.AddListener(ClearSave);
    }

    public void ClearSave()
    {
        // Очищаем сохранение (PlayerPrefs)
        PlayerPrefs.DeleteAll();
        Debug.Log("Сохранение очищено!");
        _levelManager.OnClickLoadScene("0_Menu");
    }
}
