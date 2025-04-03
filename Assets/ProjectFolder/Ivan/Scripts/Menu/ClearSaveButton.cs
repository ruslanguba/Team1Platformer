using UnityEngine;
using UnityEngine.UI;

public class ClearSaveButton : MonoBehaviour
{
    public Button clearButton;

    void Start()
    {
        // Назначаем метод на событие нажатия кнопки
        clearButton.onClick.AddListener(ClearSave);
    }

    public void ClearSave()
    {
        // Очищаем сохранение (PlayerPrefs)
        PlayerPrefs.DeleteAll();
        Debug.Log("Сохранение очищено!");
    }
}
