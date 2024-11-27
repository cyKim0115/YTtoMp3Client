using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonColorChanger : MonoBehaviour
{
    [SerializeField] private Button targetButton;
    [SerializeField] private TMP_Text targetButtonText;
    
    [Space(5)] 
    [SerializeField] private float tweenDuration = 0.3f;
    [SerializeField] private Ease tweenEase = Ease.Default;
    
    [Space(5)]
    [SerializeField] private Color activeButtonColor = new(.3922659f, .3937456f, .4056604f, 1f);
    [SerializeField] private Color inactiveButtonColor = new(.1987362f, .1987362f, .2075472f, 1f);
    [SerializeField] private Color activeButtonTextColor = new(.997f, .997f, 1f, 1f);
    [SerializeField] private Color inactiveButtonTextColor = new(.4f, .4f, .45f, 1f);

    
    public void SetButtonActive(bool active)
    {
        targetButton.interactable = active;

        var tweenSettings = new TweenSettings()
        {
            duration = tweenDuration,
            ease = tweenEase,
        };

        Tween.Color(targetButton.image, new TweenSettings<Color>(
            active ? activeButtonColor : inactiveButtonColor, tweenSettings));
        Tween.Color(targetButtonText, new TweenSettings<Color>(
            active ? activeButtonTextColor : inactiveButtonTextColor, tweenSettings));
    }

}