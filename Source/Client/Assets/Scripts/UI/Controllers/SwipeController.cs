using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField]
    private int _maxPage;
    private int _currentPage;
    private Vector3 _targetPos;
    [SerializeField]
    private Vector3 _pageStep;
    [SerializeField]
    private RectTransform _pagesRect;

    [SerializeField]
    private LeanTweenType _tweenType;
    [SerializeField]
    private float _tweenTime;

    private float _dragThreshould;
    private Vector2 originalPosition;

    private List<UICheckIcon> _checkIconList = new List<UICheckIcon>();

    private void Awake()
    {
        _currentPage = 1;
        _targetPos = _pagesRect.localPosition;
        _dragThreshould = Screen.width / 15;
    }

    public void SetNavi(List<Image> naviList)
    {
        for (int i = 0; i < naviList.Count; ++i)
        {
            var uiCheckButton = naviList[i].GetComponent<UICheckIcon>();
            uiCheckButton.Index = i;
            _checkIconList.Add(uiCheckButton);
        }
    }

    public void Previous()
    {
        if (1 == _currentPage)
            return;

        --_currentPage;
        _targetPos -= _pageStep;
        MovePage();
    }

    public void Next()
    {
        if (_currentPage >= _maxPage)
            return;

        ++_currentPage;
        _targetPos += _pageStep;
        MovePage();
    }

    private void UpdateNavi()
    {
        foreach (var icon in _checkIconList)
            icon.OnUnSelected();

        _checkIconList[_currentPage - 1].OnSelected();
    }

    public void MovePage()
    {
        _pagesRect.LeanMoveLocal(_targetPos, _tweenTime).setEase(_tweenType);
        UpdateNavi();
    }

    public void OnEndDrag(PointerEventData evt)
    {
        float movePos = Mathf.Abs(evt.position.x - evt.pressPosition.x);

        if (movePos > _dragThreshould)
        {
            if (evt.position.x > evt.pressPosition.x)
                Previous();
            else
                Next();
        }
        else
            MovePage();
    }
}
