using TMPro;
using UnityEngine.DedicatedServer;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.EventSystems;
using UnityCoreLibrary;

public class UIGameOverPopup : UIPopup
{
    enum Buttons
    {
        GoToLobby_Button,
        Retry_Button,
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.GoToLobby_Button).gameObject.BindEvent(OnClickGoToLobbyButton);
        GetButton((int)Buttons.Retry_Button).gameObject.BindEvent(OnClickRetryButton);
    }

    public void OnClickGoToLobbyButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
        CoreManagers.Scene.LoadScene(CoreDefine.Scene.Lobby);
    }

    public void OnClickRetryButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
        CoreManagers.Scene.LoadScene(CoreDefine.Scene.Game);
    }
}
