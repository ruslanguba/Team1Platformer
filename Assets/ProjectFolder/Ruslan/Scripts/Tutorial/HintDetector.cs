using System;
using UnityEngine;

public class HintDetector : MonoBehaviour
{
    public Action<string> OnHintFound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out TutorialHint tutorialHint))
        {
            OnHintFound?.Invoke(tutorialHint.GetHintText());
            tutorialHint.gameObject.SetActive(false);
        }
    }
}
