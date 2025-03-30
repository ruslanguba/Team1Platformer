using UnityEngine;
using UnityEngine.UI;

public class UIColectableImage : MonoBehaviour
{
    [SerializeField] private Image _image;
    

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void ChangeColorAlfa()
    {
        Color color = _image.color; 
        color.a = 1;
        _image.color = color;
    }
}
