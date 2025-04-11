using System;
using System.Collections;
using UnityEngine;

public class CharacterCollect : MonoBehaviour
{
    public event Action OnCollect;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private Transform _collectableMoveTarget;
    [SerializeField] private float _collectingTime;
    private AudioSource _audioSource;
    private Transform _collectableTransforn;
    private Coroutine _coroutine;

    private void Start()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out ICollectable collectable))
        {
            _audioSource.PlayOneShot(_audioClip);
            _collectableTransforn = collectable.GetCollectableTransform();
            collectable.Collect();
            StartCollecting();
            OnCollect?.Invoke();
        }
    }
    
    private void StartCollecting()
    {
        if(_coroutine == null)
        {
            _coroutine = StartCoroutine(CollectRutine());
        }
    }

    IEnumerator CollectRutine()
    {
        Vector3 startPosition = _collectableTransforn.position;
        Vector3 startScale = _collectableTransforn.localScale;

        float time = 0f;

        while (time < _collectingTime)
        {
            float t = time / _collectingTime;

            _collectableTransforn.position = Vector2.Lerp(startPosition, _collectableMoveTarget.position, t);
            _collectableTransforn.localScale = Vector2.Lerp(startScale, Vector2.zero, t);

            time += Time.deltaTime;
            yield return null;
        }

        // „тобы точно завершилось в нужной точке
        _collectableTransforn.position = _collectableMoveTarget.position;
        _collectableTransforn.localScale = Vector2.one * 0.5f;
        Destroy(_collectableTransforn.gameObject);
        _collectableTransforn = null;
        _coroutine = null;
    }
}
