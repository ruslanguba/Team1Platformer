using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject _info;
    [SerializeField] private Image _image;
    Button myButton;

    private void Awake()
    {
        myButton = GetComponent<Button>();
    }

    private void Start()
    {
        if (myButton.interactable)
        {
            _info.SetActive(true);
            _image.color = Color.green;
        }
        else
        {
            _info.SetActive(false);
            _image.color = Color.gray;
        }
    }
}
