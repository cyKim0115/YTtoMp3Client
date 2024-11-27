using TMPro;
using UnityEngine;

public class ConvertController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    [SerializeField] private UIButtonColorChanger buttonColorChanger;


    private void Start()
    {
        inputField.onValueChanged.AddListener(OnInputFieldValueChanged);

        OnInputFieldValueChanged(inputField.text);
    }

    #region Event

    private void OnInputFieldValueChanged(string str)
    {
        buttonColorChanger.SetButtonActive(!string.IsNullOrEmpty(str));
    }

    public async void OnClickConvert()
    {
        var videoUrl = inputField.text;

        if (string.IsNullOrEmpty(videoUrl))
        {
            Debug.LogError($"url이 비어있습니다.");
        }
        
        await NetworkManager.Instance.RequestConvert(videoUrl);
    }

    #endregion
}