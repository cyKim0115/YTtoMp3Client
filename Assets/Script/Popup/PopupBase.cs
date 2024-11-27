using UnityEngine;

public abstract class PopupBase<T> : MonoBehaviour where T : PopupBase<T>, IPopup
{
    public T ShowPopup<T>() where T : PopupBase<T>, IPopup
    {
        gameObject.SetActive(true);

        OnShow();
        
        return this as T;
    }

    public T GetPopup<T>() where T : PopupBase<T>, IPopup
    {
        return this as T;
    }

    protected virtual void OnShow()
    {
        
    }
}