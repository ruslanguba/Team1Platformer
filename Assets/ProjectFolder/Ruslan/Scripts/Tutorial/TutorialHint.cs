using UnityEngine;

public class TutorialHint : MonoBehaviour
{
    [SerializeField] private int _hintId;
    [SerializeField] protected string _hintText = "No Hint!!!";  

    public string GetHintText()
    {
        return _hintText;
    }
}
