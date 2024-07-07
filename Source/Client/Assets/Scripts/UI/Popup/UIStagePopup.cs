using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityCoreLibrary;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class UIStagePopup : UIPopup
{
    [SerializeField]
    private int PAGE_MAX_STAGE = 20;

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
    private List<UIBase> _stageList = new List<UIBase>();
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

        Managers.StageData.AddListener(nameof(StageData.IsOpen), OnUpdateOpen);
        Managers.StageData.AddListener(nameof(StageData.StartCount), OnUpdateStarCount);
    }

    public void OnUpdateOpen(int index)
    {
        if (-1 == index)
            return;

        var sibilingIndex = _stageList[index].gameObject.DestroyAndGetSiblingIndex();
        _stageList.RemoveAt(index);

        AddStage(Managers.StageData.GetStageData(index));

        _stageList[index].transform.SetSiblingIndex(sibilingIndex);
    }

    public void OnUpdateStarCount(int index)
    {
        if (-1 == index)
            return;

        ((UIOpenStage)_stageList[index]).SetStarCount(Managers.StageData.GetStarCount(index));
    }

    public void Clear()
    {
        _gird = GetHorizontalLayoutGroup();

        Util.DeleteAllChildrens(_pageNavi);

        var stageData = Managers.StageData.GetStageData();
        var stageCount = stageData.Length;
        if (0 == stageCount)
            return;

        for (var i = 0; i < stageCount; ++i)
        {
            if (0 == (i % PAGE_MAX_STAGE))
                AddStagePage();

            AddStage(stageData[i]);
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


    private void AddStage(StageData stageData)
    {
        if (stageData.IsOpen)
        {
            var openStage = AddOpenStage(stageData.StageID);
            openStage.SetStageID(stageData.StageID);
            openStage.SetStarCount(stageData.StartCount);
            openStage.SetMaxStarCount(Managers.StageData.GetMaxStarCount(stageData.StageID));
        }
        else
            AddLockStage(stageData.StageID);
    }

    private void AddLockStage(int index)
    {
        GameObject go = CoreManagers.Resource.Instantiate("UI/Stage/UILockStage", _pageList[GetPage(index)].transform);
        _stageList.Add(go.GetOrAddComponent<UILockStage>());
    }

    private UIOpenStage AddOpenStage(int index)
    {
        GameObject go = CoreManagers.Resource.Instantiate("UI/Stage/UIOpenStage", _pageList[GetPage(index)].transform);
        var openStage = go.GetOrAddComponent<UIOpenStage>();
        _stageList.Insert(index, openStage);
        return openStage;
    }

    private int GetPage(int index)
    {
       return index / PAGE_MAX_STAGE;
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
