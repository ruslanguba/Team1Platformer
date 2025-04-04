using UnityEngine;
using UnityEngine.UI;

public class ClearSaveButton : MonoBehaviour
{
    [SerializeField] private Button _clearButton;
    [SerializeField] private LevelManager _levelManager;

    void Start()
    {
        // ��������� ����� �� ������� ������� ������
        _clearButton.onClick.AddListener(ClearSave);
    }

    public void ClearSave()
    {
        // ������� ���������� (PlayerPrefs)
        PlayerPrefs.DeleteAll();
        Debug.Log("���������� �������!");
        _levelManager.OnClickLoadScene("0_Menu");
    }
}
