using System.Collections.Generic;
using UnityEngine;
using YDLib;

public class ListController : MonoBehaviour
{
    [SerializeField] private UIButtonColorChanger buttonColorChanger;

    private List<FileItem> listItem = new();
    
    #region Evnet

    public async void OnClickGetList()
    {
        listItem.Clear();
        
        listItem.AddRange(await NetworkManager.Instance.RequestList());
    }
    
    #endregion
}