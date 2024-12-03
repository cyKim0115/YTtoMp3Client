using UnityEngine;

public class ListController : MonoBehaviour
{
    [SerializeField] private UIButtonColorChanger buttonColorChanger;

    private void SetGetListButtonColor(bool active)
    {
        buttonColorChanger.SetButtonActive(active);
    }
    
    #region Evnet

    private void OnEnable()
    {
        NetworkManager.Instance.ConnectionStateChanged += SetGetListButtonColor;
    }

    public async void OnClickGetList()
    {
        var popup = PopupManager.Instance.ShowPopup<ListPopup>();
        popup.ShowWithAnimation();
    }
    
    #endregion
}