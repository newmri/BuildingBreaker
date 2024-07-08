using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityCoreLibrary;
using static UnityEngine.EventSystems.EventTrigger;

public class UILobbyScene : UIScene
{
    enum TextMeshProUGUIs
    {
        NickName_Text,
        Level_Text,
        EXP_Text,
        Coin_Text,
        Energy_Text,
        Key_Text,
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

    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
        Bind<Button>(typeof(Buttons));
        Bind<Slider>(typeof(Sliders));

        this.GetTextMesh((int)TextMeshProUGUIs.NickName_Text).text = Managers.UserData.NickName;

        GetButton((int)Buttons.Setting_Button).gameObject.BindEvent(OnClickSettingButton);
        GetButton((int)Buttons.Play_Button).gameObject.BindEvent(OnClickPlayButton);

        Managers.UserData.AddListener(nameof(Managers.UserData.Level), OnUpdateLevel);
        Managers.UserData.AddListener(nameof(Managers.UserData.EXP), OnUpdateEXP);

        Managers.UserData.AddListener(nameof(Managers.UserData.Coin), OnUpdateCoin);
        Managers.UserData.AddListener(nameof(Managers.UserData.Energy), OnUpdateEnergy);
        Managers.UserData.AddListener(nameof(Managers.UserData.Key), OnUpdateKey);

    }

    public void OnClickSettingButton(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UISettingPopup>();
    }

    public void OnClickPlayButton(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UIStagePopup>();
    }

    public void OnUpdateLevel(int index)
    {
        this.GetTextMesh((int)TextMeshProUGUIs.Level_Text).text = Managers.UserData.Level.ToString();
    }

    public void OnUpdateEXP(int index)
    {
        var slider = GetSlider((int)Sliders.EXP_Slider);
        slider.maxValue = Managers.UserData.GetMaxExp(Managers.UserData.Level);
        slider.value = Managers.UserData.EXP;
        this.GetTextMesh((int)TextMeshProUGUIs.EXP_Text).text = slider.value.ToString() + " / " + slider.maxValue.ToString();
    }

    public void OnUpdateCoin(int index)
    {
        this.GetTextMesh((int)TextMeshProUGUIs.Coin_Text).text = Managers.UserData.Coin.ToString();
    }

    public void OnUpdateEnergy(int index)
    {
        this.GetTextMesh((int)TextMeshProUGUIs.Energy_Text).text = Managers.UserData.Energy.ToString() + " / " + Managers.UserData.GetMax(nameof(UserData.Energy));
    }

    public void OnUpdateKey(int index)
    {
        this.GetTextMesh((int)TextMeshProUGUIs.Key_Text).text = Managers.UserData.Key.ToString() + " / " + Managers.UserData.GetMax(nameof(UserData.Key));
    }

}
