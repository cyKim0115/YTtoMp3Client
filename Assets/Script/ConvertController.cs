using System.Net;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

public class ConvertController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _convertButton;
    [SerializeField] private TMP_Text _convertButtonText;

    [Space(5)] [SerializeField] private float _tweenDuration = 0.2f;
    [SerializeField] private Ease _tweenEase = Ease.Linear;

    private Color _activeButtonColor = new Color(.3922659f, .3937456f, .4056604f, 1f);
    private Color _inactiveButtonColor = new Color(.1987362f, .1987362f, .2075472f, 1f);
    private Color _activeButtonTextColor = new Color(.997f, .997f, 1f, 1f);
    private Color _inactiveButtonTextColor = new Color(.4f, .4f, .45f, 1f);


    private void Start()
    {
        _inputField.onValueChanged.AddListener(OnInputFieldValueChanged);

        OnInputFieldValueChanged(_inputField.text);
    }

    private void SetConvertButtonActive(bool active)
    {
        _convertButton.interactable = active;

        TweenSettings tweenSettings = new TweenSettings()
        {
            duration = _tweenDuration,
            ease = _tweenEase,
        };

        Tween.Color(_convertButton.image, new TweenSettings<Color>(
            active ? _activeButtonColor : _inactiveButtonColor, tweenSettings));
        Tween.Color(_convertButtonText, new TweenSettings<Color>(
            active ? _activeButtonTextColor : _inactiveButtonTextColor, tweenSettings));
    }

    #region Event

    private void OnInputFieldValueChanged(string str)
    {
        SetConvertButtonActive(!string.IsNullOrEmpty(str));
    }

    public void OnClickConvert()
    {
        ConvertInfo convertInfo = new ConvertInfo()
        {
            url = _inputField.text,
        };
        
        WebRequest request = WebRequest.Create("http://localhost:9227/Convert");
        request.Method = "POST";
        request.ContentType = "application/json;charset=UTF-8";
        var postData = JsonUtility.ToJson(convertInfo);
        var byteArray = Encoding.UTF8.GetBytes(postData);

        request.ContentLength = byteArray.Length;
        var dataStream = request.GetRequestStream();
        
        dataStream.Write(byteArray, 0, byteArray.Length);;
        dataStream.Close();

        request.GetResponse();
    }

    #endregion
}