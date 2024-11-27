using System.Collections.Generic;
using UnityEngine;

public class PopupManager : SingletonObject<PopupManager>
{
    private Dictionary<string, IPopup> _dicPopup; 
    
    public T ShowPopup<T>() where T : PopupBase<T>, IPopup
    {
        if (_dicPopup == null) _dicPopup = new Dictionary<string, IPopup>();

        string typeName = typeof(T).Name;
        if (!_dicPopup.TryGetValue(typeName, out var popup))
        {
            GameObject insObj = Instantiate(Resources.Load<GameObject>($"Popup/{typeName}"));
            
            var getCheck = insObj.TryGetComponent(out popup);
            if (!getCheck)
                Debug.LogError($"PopupManager : 생성한 오브젝트에 팝업 컴포넌트가 없습니다. {typeName}");

            _dicPopup.Add(typeName, popup);
        }

        popup.ShowPopup<T>();
        
        return popup.GetPopup<T>();
    }
}