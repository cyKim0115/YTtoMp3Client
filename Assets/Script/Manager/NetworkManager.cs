using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using YDLib;

public class NetworkManager : SingletonObject<NetworkManager>
{
    private bool _connectionCheck = false;

    public bool ConnectionCheck => _connectionCheck;

    public UnityAction<bool> ConnectionStateChanged;


    public async UniTask RequestConvert(string videoUrl)
    {
        var convertInfo = new ConvertRequest()
        {
            url = videoUrl,
        };

        WebRequest request = WebRequest.Create($"{ConnectionConfigManager.Instance.GetBaseUrl()}/Convert");
        request.Method = "POST";
        request.ContentType = "application/json;charset=UTF-8";
        var postData = JsonUtility.ToJson(convertInfo);
        var byteArray = Encoding.UTF8.GetBytes(postData);

        request.ContentLength = byteArray.Length;
        var dataStream = await request.GetRequestStreamAsync();

        dataStream.WriteAsync(byteArray, 0, byteArray.Length).GetAwaiter().GetResult();
        dataStream.Close();

        var response = await request.GetResponseAsync();
    }

    public async UniTask<List<FileItem>> RequestList()
    {
        WebRequest request = WebRequest.Create($"{ConnectionConfigManager.Instance.GetBaseUrl()}/List");
        request.Method = "POST";
        request.ContentType = "application/json;charset=UTF-8";
        var emptyStringBytes = Encoding.UTF8.GetBytes("");
        request.ContentLength = emptyStringBytes.Length;
        var dataStream = await request.GetRequestStreamAsync();

        dataStream.WriteAsync(emptyStringBytes, 0, emptyStringBytes.Length).GetAwaiter().GetResult();
        dataStream.Close();

        List<FileItem> listFileItem = new List<FileItem>();
        var response = await request.GetResponseAsync();
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
            string result = await reader.ReadToEndAsync();
            // listFileItem = JsonUtility.FromJson<List<FileItem>>(result);
            listFileItem = JsonConvert.DeserializeObject<List<FileItem>>(result);
        }

        return listFileItem;
    }

    public async UniTask<bool> RequestCheck()
    {
        WebRequest request = WebRequest.Create($"{ConnectionConfigManager.Instance.GetBaseUrl()}/Check");
        request.Method = "GET";
        request.ContentType = "application/json;charset=UTF-8";

        using var response = (HttpWebResponse)(await request.GetResponseAsync());

        return response != null && response.StatusCode == HttpStatusCode.OK;
    }


    public async UniTask RequestDownload(string fileName)
    {
        var url = $"{ConnectionConfigManager.Instance.GetBaseUrl()}/Download/{fileName}";
        string encodedUrl = UnityWebRequest.EscapeURL(url);
        
        Debug.LogError(encodedUrl);
        
        Application.OpenURL(encodedUrl);
    }
}