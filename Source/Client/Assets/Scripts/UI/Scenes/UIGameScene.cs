using System.Collections;
using System.Collections.Generic;
using UnityCoreLibrary;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIGameScene : UIScene
{
    enum Buttons
    {
        Setting_Button,
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

    }

    public void OnClickSettingButton(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UISettingPopup>();
    }
}
