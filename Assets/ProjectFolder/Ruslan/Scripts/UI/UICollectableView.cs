using UnityEngine;

public class UICollectableView : MonoBehaviour
{
    [SerializeField] private UIColectableImage[] _images;
    private CollectHandler _collectHandler;

    private void Awake()
    {
        _collectHandler = FindFirstObjectByType<CollectHandler>();
    }

    //public void Bind(CollectHandler collectHandler)
    //{
    //    _collectHandler = collectHandler;
    //    collectHandler.OnCollectValueChanged += ChangeImage;
    //}

    private void OnEnable()
    {
        _collectHandler.OnCollectValueChanged += ChangeImage;
    }

    private void OnDisable()
    {
        _collectHandler.OnCollectValueChanged -= ChangeImage;
    }

    private void Start()
    {
        _images = GetComponentsInChildren<UIColectableImage>();
    }

    private void ChangeImage(int imageIndex)
    {
        if (imageIndex < _images.Length)
        {
            _images[imageIndex].ChangeColorAlfa();
        }
    }
}
