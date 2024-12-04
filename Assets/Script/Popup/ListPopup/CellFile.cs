using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using YDLib;

public class CellFile : MonoBehaviour
{
    [SerializeField] private TMP_Text txtName;

    private FileItem data;
    
    public void Init(FileItem fileItem)
    {
        data = fileItem;
        
        txtName.text = $"{fileItem.fileName} ({fileItem.size})";
    }

    public void OnClickDownload()
    {
        NetworkManager.Instance.RequestDownload(data.fileName).Forget();
    }
}
