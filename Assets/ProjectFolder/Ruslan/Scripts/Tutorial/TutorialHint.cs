using UnityEngine;

public class TutorialHint : MonoBehaviour
{
    [SerializeField] private int _hintId;
    [SerializeField] private string _hintText = "No Hint!!!";  

    public string GetHintText()
    {
        return _hintText;
    }
}
