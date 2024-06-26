using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISettingPopup : UIPopup
{
    enum Buttons
    {
        Back_Button,
        Language_Button,
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.Back_Button).gameObject.BindEvent(OnClickBackButton);
        GetButton((int)Buttons.Language_Button).gameObject.BindEvent(OnClickLanguageButton);
    }

    public void OnClickBackButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }

    public void OnClickLanguageButton(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UILanguagePopup>();
    }
}
