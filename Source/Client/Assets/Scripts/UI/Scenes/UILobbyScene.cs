using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityCoreLibrary;

public class UILobbyScene : UIScene
{
    enum TextMeshProUGUIs
    {
        NickName_Text,
        Level_Text,
        EXP_Text
    }

    enum Buttons
    {
        Setting_Button,
        Play_Button,
    }

    enum Sliders
    {
        EXP_Slider,
    }

    private TextMeshProUGUI _level;

    private TextMeshProUGUI _expText;
    private Slider _expSlider;

    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
        Bind<Button>(typeof(Buttons));
        Bind<Slider>(typeof(Sliders));

        this.GetTextMesh((int)TextMeshProUGUIs.NickName_Text).text = Managers.UserData.NickName;

        _level = this.GetTextMesh((int)TextMeshProUGUIs.Level_Text);
        _expSlider = GetSlider((int)Sliders.EXP_Slider);
        _expText = this.GetTextMesh((int)TextMeshProUGUIs.EXP_Text);

        Managers.UserData.AddListener(nameof(Managers.UserData.Level), OnUpdateLevel);
        Managers.UserData.AddListener(nameof(Managers.UserData.EXP), OnUpdateEXP);

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

    public void OnUpdateLevel()
    {
        _level.text = Managers.UserData.Level.ToString();
    }

    public void OnUpdateEXP()
    {
        _expSlider.maxValue = 1;
        _expSlider.value = Managers.UserData.EXP;
        _expText.text = _expSlider.value.ToString() + "/" + _expSlider.maxValue.ToString();
    }
}
