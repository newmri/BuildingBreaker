using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UILobbyScene : UIScene
{
    enum Buttons
    {
        Setting_Button,
        Play_Button,
    }

    enum TextMeshProUGUIs
    {
        Text_TapToStart
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.Setting_Button).gameObject.BindEvent(OnClickSettingButton);
        GetButton((int)Buttons.Play_Button).gameObject.BindEvent(OnClickPlayButton);
    }

    public void OnClickSettingButton(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UISettingPopup>();
    }

    public void OnClickPlayButton(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UIStagePopup>();
    }
}
