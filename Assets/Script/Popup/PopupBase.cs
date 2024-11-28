using UnityEngine;

public abstract class PopupBase<T> : MonoBehaviour where T : PopupBase<T>, IPopup
{
    private GameObject _cachedGameObject;
    
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

    public bool IsActive
    {
        get
        {
            if (_cachedGameObject == null)
                _cachedGameObject = gameObject;

            return _cachedGameObject;
        }
    }
    
    protected virtual void OnShow()
    {
        
    }
}