using System.Collections;
using UnityEngine;

public class UICollectableView : MonoBehaviour
{
    [SerializeField] private UIColectableImage[] _images;
    [SerializeField] private GameObject _collectablesPanel;
    [SerializeField] private float _hideDelay;
    private CollectHandler _collectHandler;

    private void Awake()
    {
        _collectHandler = FindFirstObjectByType<CollectHandler>();
    }

    private void OnEnable()
    {
        _collectHandler.OnCollectValueChanged += CollectCollectable;
    }

    private void OnDisable()
    {
        _collectHandler.OnCollectValueChanged -= CollectCollectable;
    }

    private void Start()
    {
        _images = GetComponentsInChildren<UIColectableImage>();
        _collectablesPanel = _images[0].transform.parent.gameObject;
        _collectablesPanel.SetActive(false);
    }

    private void CollectCollectable(int imageIndex)
    {
        StartCoroutine(ShowCollecPanel());
        ChangeImage(imageIndex);
    }

    IEnumerator ShowCollecPanel()
    {
        _collectablesPanel.SetActive(true);
        yield return new WaitForSeconds(_hideDelay);
        _collectablesPanel.SetActive(false);
    }

    private void ChangeImage(int imageIndex)
    {
        if (imageIndex < _images.Length)
        {
            _images[imageIndex].ChangeColorAlfa();
        }
    }
}
