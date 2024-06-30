using System;
using System.Collections.Generic;
using UnityCoreLibrary;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class UIStagePopup : UIPopup
{
    [SerializeField]
    private int MAX_STAGE = 20;

    enum GameObjects
    {
        Page_Scroll,
        PageNavi,
    }

    enum Buttons
    {
        Close_Button,
        Prev_Button,
        Next_Button,
    }

    private List<UIPage> _pageList = new List<UIPage>();
    private List<UILockStage> _lockList = new List<UILockStage>();
    private List<UIOpenStage> _openList = new List<UIOpenStage>();
    private GameObject _pageNavi;

    private GameObject _gird;

    private SwipeController _swipeController;

    private bool _isLoaded = false;
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));

        _swipeController = GetObject((int)GameObjects.Page_Scroll).GetComponent<SwipeController>();
        _pageNavi = GetObject((int)GameObjects.PageNavi);

        GetButton((int)Buttons.Close_Button).gameObject.BindEvent(OnClickCloseButton);
        GetButton((int)Buttons.Prev_Button).gameObject.BindEvent(OnClickPrevButton);
        GetButton((int)Buttons.Next_Button).gameObject.BindEvent(OnClickNextButton);

        Clear();
    }

    public void Clear()
    {
        _gird = GetHorizontalLayoutGroup();

        Util.DeleteAllChildrens(_pageNavi);

        AddStagePage();
        AddOpenStage();

        int stageCount = Managers.StageData.GetStageCount();

        for (int i = 1; i < stageCount; ++i)
        {
            if (0 == (i % MAX_STAGE))
                AddStagePage();

            AddLockStage();
        }

        if(0 < _pageList.Count)
        {
            Vector3 pageStep = new Vector3(_pageList[0].GetComponent<RectTransform>().rect.width, 0, 0);

            _swipeController.SetPageStep(-pageStep);

        }

        _swipeController.SetNavi(Util.FindAllChildrens<Image>(_pageNavi));
    }

    public void OnClickCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }

    public void AddStagePage()
    {
        GameObject go = CoreManagers.Resource.Instantiate("UI/UIPage", _gird.transform);
        _pageList.Add(go.GetOrAddComponent<UIPage>());

        CoreManagers.Resource.Instantiate("UI/UIPageNavi", _pageNavi.gameObject.transform);
    }


    public void AddLockStage()
    {
        GameObject go = CoreManagers.Resource.Instantiate("UI/Stage/UILockStage", _pageList[_pageList.Count - 1].transform);
        _lockList.Add(go.GetOrAddComponent<UILockStage>());
    }

    public void AddOpenStage()
    {
        GameObject go = CoreManagers.Resource.Instantiate("UI/Stage/UIOpenStage", _pageList[_pageList.Count - 1].transform);
        _openList.Add(go.GetOrAddComponent<UIOpenStage>());
    }

    public void OnClickPrevButton(PointerEventData evt)
    {
        _swipeController.Previous();
    }

    public void OnClickNextButton(PointerEventData evt)
    {
        _swipeController.Next();
    }
}
