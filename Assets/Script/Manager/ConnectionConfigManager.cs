using UnityEngine;

public class ConnectionConfigManager : SingletonObject<ConnectionConfigManager>
{
    private readonly string ConnectIpKey = "connect_ip";
    private readonly string ConnectPortKey = "connect_port";

    public string GetBaseUrl()
    {
        return "http://192.168.0.58:9227";
        // return $"http://{GetValue(ConnectIpKey)}:{GetValue(ConnectPortKey)}";
    }

    private string GetValue(string key)
    {
        if (PlayerPrefs.HasKey(key))
            PlayerPrefs.SetString(key, "");

        return PlayerPrefs.GetString(key);
    }

    private void SetValue(string key, string value)
    {
        if (!PlayerPrefs.HasKey(key))
            PlayerPrefs.SetString(key, "");

        PlayerPrefs.SetString(key, value);
    }
}