using UnityEngine;

public class SpiritSprite : MonoBehaviour
{
    [SerializeField] private GameObject _greenEyes;
    [SerializeField] private GameObject _redEyes;
    //[SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _eyeRenderer;

    public void CheckDirection(Vector2 targetPoint)
    {
        //_spriteRenderer.flipX = transform.position.x < targetPoint.x;
        if(transform.position.x < targetPoint.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
    }

    public void CheckTarget(bool isTargetCharacter, Vector2 target)
    {
        CheckDirection(target);
        if (isTargetCharacter)
            SetRedEyes();
        else
            SetGreenEyes();
    }

    private void SetGreenEyes()
    {
        _greenEyes.SetActive(true);
        _redEyes.SetActive(false);
        _eyeRenderer.color = Color.green;
    }

    private void SetRedEyes()
    {
        _greenEyes.SetActive(false);
        _redEyes.SetActive(true);
        _eyeRenderer.color = Color.red;
    }
}
