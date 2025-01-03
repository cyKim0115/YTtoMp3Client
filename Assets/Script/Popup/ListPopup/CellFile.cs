using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using YDLib;

public class CellFile : MonoBehaviour
{
    [SerializeField] private Text txtName;

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
