using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireSpriteRandomiser : MonoBehaviour
{
    [SerializeField] private Sprite[] _fireSpritesList;
    [SerializeField] private float _changeTime = 1;
    [SerializeField] private bool _isBurning = true;
    private SpriteRenderer _spriteRenderer;
    private Sprite _currentSprite;


    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentSprite = _spriteRenderer.sprite;
        StartCoroutine(SpriteChange());
    }
    
    private void RandomiseSprite()
    {
        int rnd = Random.Range(0, _fireSpritesList.Length);
        int rndFlip = Random.Range(0, 2);
        _spriteRenderer.sprite = _fireSpritesList[rnd];
        if (rndFlip == 1 )
            _spriteRenderer.flipX = true;
        else 
            _spriteRenderer.flipX = false;
    }

    private IEnumerator SpriteChange()
    {
        while (_isBurning)
        {
            yield return new WaitForSeconds(_changeTime);
            RandomiseSprite();

        }
    }
}
