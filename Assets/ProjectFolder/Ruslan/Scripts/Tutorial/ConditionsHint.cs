using UnityEngine;

public class ConditionsHint : TutorialHint
{
    [SerializeField] private string _noFireText = "Нужно чтобы сердце горело. Иначе мне не спасти учителя!";
    [SerializeField] private string _noBonfireText = "Духи этого леса не пустят меня дальше! Нужно зажечь алтари, чтобы их успокоить...";

    public void SetHintText(bool isNoBonfire)
    {
        _hintText = isNoBonfire ? _noBonfireText : _noFireText;
    }
}
