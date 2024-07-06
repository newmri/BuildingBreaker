using UnityCoreLibrary;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using System.Collections;
using UnityEngine.Localization.Settings;

public class TitleScene : BaseScene
{
    private void Awake()
    {
        Init(CoreDefine.Scene.Title);
    }

    protected override void Init(CoreDefine.Scene scene)
    { 
        base.Init(scene);

        Managers.UI.ShowSceneUI<UITitleScene>();

        if(false == Managers.UserData.IsLoaded)
        {
            var popup = Managers.UI.ShowPopupUI<UINameInputPopup>();
            popup.SetText(new LocalizationInfo("UI", "NickName"), new LocalizationInfo("UI", "NickName"), new LocalizationInfo("UI", "NickNameMakeInfo"));
        }
    }

    public override void Clear()
    {
        Managers.UI.Clear();
    }
}

