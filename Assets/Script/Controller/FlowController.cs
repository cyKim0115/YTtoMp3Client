using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FlowController : MonoBehaviour
{
    private void Start()
    {
        Flow().Forget();
    }

    private async UniTaskVoid Flow()
    {
        Application.targetFrameRate = 60;
        
        await ConnectCheckProcess();
    }

    private async UniTask ConnectCheckProcess()
    {
        while (!await UrlAvailableCheck())
        {
            var popup = PopupManager.Instance.ShowPopup<ConnectSettingPopup>();
            popup.ShowWithAnimation();

            await UniTask.Delay(TimeSpan.FromSeconds(1.0f));

            await UniTask.WaitUntil(() => !popup.IsActive);
            
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
        }
    }

    private async UniTask<bool> UrlAvailableCheck()
    {
        string ipSetting = ConnectionConfigManager.Instance.GetIpSetting();

        if (string.IsNullOrEmpty(ipSetting))
            return false;

        if (ipSetting.ToCharArray().Count(x => x == '.') < 3)
            return false;

        string portSetting = ConnectionConfigManager.Instance.GetPortSetting();
        if (string.IsNullOrEmpty(portSetting))
            return false;

        if (!await NetworkManager.Instance.RequestCheck())
            return false;

        return true;
    }
}