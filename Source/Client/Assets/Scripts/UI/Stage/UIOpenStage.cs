using TMPro;
using UnityCoreLibrary;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIOpenStage : UIBase
{
    enum TextMeshProUGUIs
    {
        StageID_Text,
    }

    enum GameObjects
    {
        Stars,
    }

    enum Buttons
    {
        UIOpenStage,
    }

    private int _stageID;
    private byte _starCount;
    private byte _maxStarCount;

    private bool isLoaded = false;
    public override void Init()
    {
        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));

        this.GetTextMesh((int)TextMeshProUGUIs.StageID_Text).text = (_stageID + 1).ToString();

        GetButton((int)Buttons.UIOpenStage).gameObject.BindEvent(OnClickButton);

        UpdateStarCount();

        isLoaded = true;
    }

    private void UpdateStarCount()
    {
        Util.DeleteAllChildrens(GetObject((int)GameObjects.Stars));

        for (var i = 0; i < _maxStarCount; ++i)
        {
            if (i < _starCount)
                CoreManagers.Resource.Instantiate("UI/Stage/Stars/On", GetObject((int)GameObjects.Stars).gameObject.transform);
            else
                CoreManagers.Resource.Instantiate("UI/Stage/Stars/Off", GetObject((int)GameObjects.Stars).gameObject.transform);
        }
    }

    public void SetStageID(int stageID)
    {
        _stageID = stageID;
    }

    public void SetStarCount(byte starCount)
    {
        _starCount = starCount;

        if(isLoaded)
            UpdateStarCount();
    }

    public void SetMaxStarCount(byte maxStarCount)
    {
        _maxStarCount = maxStarCount;
    }

    public void OnClickButton(PointerEventData evt)
    {
        Managers.StageData.CurrentStageID = _stageID;
        CoreManagers.Scene.LoadScene(CoreDefine.Scene.Game);
    }
}
