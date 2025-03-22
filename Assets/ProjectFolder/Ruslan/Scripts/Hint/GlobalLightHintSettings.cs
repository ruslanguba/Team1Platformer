using UnityEngine;

[CreateAssetMenu(fileName = "GlobalLightHintSettings", menuName = "Scriptable Objects/GlobalLightHintSettings")]
public class GlobalLightHintSettings : ScriptableObject
{
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _spiritualColor;

    public Color NormalColor => _normalColor;
    public Color SpiritualColor => _spiritualColor;
}
