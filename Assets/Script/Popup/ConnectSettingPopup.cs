using UnityEngine;
using UnityEngine.UI;

public class ConnectSettingPopup : PopupBase<ConnectSettingPopup>, IPopup
{
    [SerializeField] private InputField ipInputField;
    [SerializeField] private InputField portInputField;

    private void Init()
    {
        
    }
}