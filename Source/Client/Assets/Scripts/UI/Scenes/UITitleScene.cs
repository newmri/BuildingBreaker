using System.Collections;
using System.Collections.Generic;
using UnityCoreLibrary;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UITitleScene : UIScene
{
    enum Buttons
    {
       Start_Button
    }

    enum TextMeshProUGUIs
    {
        TapToStart_Text
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));

        GetButton((int)Buttons.Start_Button).gameObject.BindEvent(OnEnterButton, CoreDefine.UIEvent.Enter);
        GetButton((int)Buttons.Start_Button).gameObject.BindEvent(OnExitButton, CoreDefine.UIEvent.Exit);
        GetButton((int)Buttons.Start_Button).gameObject.BindEvent(OnClickStartButton);
    }

    public void OnEnterButton(PointerEventData evt)
    {
        this.GetTextMesh((int)TextMeshProUGUIs.TapToStart_Text).color = Color.green;
    }

    public void OnExitButton(PointerEventData evt)
    {
        this.GetTextMesh((int)TextMeshProUGUIs.TapToStart_Text).color = Color.white;
    }

    public void OnClickStartButton(PointerEventData evt)
    {
        CoreManagers.Scene.LoadScene(CoreDefine.Scene.Lobby);
    }
}
