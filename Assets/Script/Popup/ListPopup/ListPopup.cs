
using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class ListPopup : PopupBase<ListPopup>, IPopup
{
    [Header("연출 설정")]
    [Space(5)]
    [SerializeField] private Image imgBackground;
    [SerializeField] private TweenInfo bgColorTween_On;
    [SerializeField] private TweenInfo bgColorTween_Off;

    [Space(5)]
    [SerializeField] private RectTransform rtPanel;
    [SerializeField] private TweenInfo panelMoveTween_On;
    [SerializeField] private TweenInfo panelMoveTween_Off;

    [Header("---------------------------------")] 
    [Header("연결")] 
    [Space(5)] 
    [SerializeField] private RectTransform rtCellParent;
    [SerializeField] private ScrollRect scrollRect;

    private const string _cellPrefabPath = "Cell/CellFile";
    private GameObject _cachedCellPrefab;
    
    private readonly List<CellFile> _listCell = new();
    private readonly List<YDLib.FileItem> _listData = new();

    private CancellationTokenSource _cts;

    private async UniTask Init()
    {
        _listData.Clear();

        _listData.AddRange(await NetworkManager.Instance.RequestList());
        
        ResetList();
    }

    private void ResetList()
    {
        scrollRect.normalizedPosition = Vector2.zero;
        
        _listCell.Clear();
        
        _listCell.ForEach(cell=>cell.gameObject.SetActive(false));

        for (int i = _listCell.Count; i < _listData.Count; i++)
        {
            if(_cachedCellPrefab == null)
                _cachedCellPrefab =  Resources.Load<GameObject>(_cellPrefabPath);
            GameObject insCell = Instantiate(_cachedCellPrefab);

            insCell.transform.SetParent(rtCellParent);
            insCell.TryGetComponent(out CellFile cellFile);
            
            _listCell.Add(cellFile);
        }

        for (int i = 0; i < _listCell.Count; i++)
        {
            _listCell[i].Init(_listData[i]);
            _listCell[i].gameObject.SetActive(true);
        }
    }
    
    #region Popup

    public void ShowWithAnimation()
    {
        Init().Forget();
        
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        
        // Color
        Tween.Alpha(imgBackground, bgColorTween_On.value, bgColorTween_On.duration, bgColorTween_On.ease).WithCancellation(_cts.Token);

        // Panel Position
        Tween.UIAnchoredPositionY(rtPanel, panelMoveTween_On.value, panelMoveTween_On.duration, panelMoveTween_On.ease).WithCancellation(_cts.Token);
    }

    public async UniTask HideWithAnimation()
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();

        // Color
        Tween.Alpha(imgBackground, bgColorTween_Off.value, bgColorTween_Off.duration, bgColorTween_Off.ease).WithCancellation(_cts.Token);

        // Panel Position
        Tween.UIAnchoredPositionY(rtPanel, panelMoveTween_Off.value, panelMoveTween_Off.duration,
            panelMoveTween_Off.ease).WithCancellation(_cts.Token);

        await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: _cts.Token);

        gameObject.SetActive(false);
    }

    #endregion

    #region Event

    public void OnClickClose()
    {
        HideWithAnimation().Forget();
    }

    #endregion

    [Serializable]
    public class TweenInfo
    {
        public float duration;
        public float value;
        public Ease ease;
    }
}