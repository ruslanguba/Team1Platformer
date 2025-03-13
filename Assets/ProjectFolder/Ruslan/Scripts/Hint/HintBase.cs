using UnityEngine;

public abstract class HintBase : MonoBehaviour, IHint
{
    public virtual void ShowHint(float duration) { }
}
