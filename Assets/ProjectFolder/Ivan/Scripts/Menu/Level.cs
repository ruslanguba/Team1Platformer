using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{
    [Header("Номер уровня")]
    [SerializeField] private int _level;

    [Header("Настройка звезд")]
    [Tooltip("Ограничение по времени в минутах")]
    [SerializeField] private float _timeLimit = 5;

    [Header("Ссылки в префабе")]
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private Image _Scrin;
    [SerializeField] private TextMeshProUGUI _textInfo;

    [Header("Звезды")]
    [SerializeField] private Image[] starImage;

    private Button _myButton;
    private LoadData _loadData;

    private void Awake()
    {
        _myButton = GetComponent<Button>();
        _loadData = FindObjectOfType<LoadData>();      
    }

    private void Start()
    {
        
        if (_myButton.interactable)
        {
            ActiveButton();
            OpenStar();
        }
        else
        {
            InactiveButton();
        }
    } 
    void ActiveButton()
    {
        _infoPanel.SetActive(true);

        int minutes = Mathf.FloorToInt(_loadData.LoadTimer(_level) / 60);//Переводим время в минуты и секунды
        int seconds = Mathf.FloorToInt(_loadData.LoadTimer(_level) % 60);
        //Выводим в текстовую формочку
        _textInfo.text = $"Время: {minutes:00}:{seconds:00} \nСмертей: {_loadData.LoadDeths(_level)}\nИдолы: {_loadData.LoadCollect(_level)}/{3}";

        _Scrin.color = Color.white;//Меняем цвет
    }
    void InactiveButton()
    {
        _infoPanel.SetActive(false);
        _Scrin.color = Color.gray;
    }
    
    void OpenStar()
    {
        for (int i = 0; i < starImage.Length; i++)
        {
            starImage[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < _loadData.LoadStar(_level); i++)
        {
            starImage[i].gameObject.SetActive(true);
        }
    }
}
