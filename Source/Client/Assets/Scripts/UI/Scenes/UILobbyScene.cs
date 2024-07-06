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

    private TextMeshProUGUI _level;

    private TextMeshProUGUI _expText;
    private Slider _expSlider;

    private TextMeshProUGUI _coin;
    private TextMeshProUGUI _energy;
    private TextMeshProUGUI _key;

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

        _coin = this.GetTextMesh((int)TextMeshProUGUIs.Coin_Text);
        _energy = this.GetTextMesh((int)TextMeshProUGUIs.Energy_Text);
        _key = this.GetTextMesh((int)TextMeshProUGUIs.Key_Text);

        Managers.UserData.AddListener(nameof(Managers.UserData.Level), OnUpdateLevel);
        Managers.UserData.AddListener(nameof(Managers.UserData.EXP), OnUpdateEXP);

        Managers.UserData.AddListener(nameof(Managers.UserData.Coin), OnUpdateCoin);
        Managers.UserData.AddListener(nameof(Managers.UserData.Energy), OnUpdateEnergy);
        Managers.UserData.AddListener(nameof(Managers.UserData.Key), OnUpdateKey);

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
        _expSlider.maxValue = Managers.UserData.GetMaxExp(Managers.UserData.Level);
        _expSlider.value = Managers.UserData.EXP;
        _expText.text = _expSlider.value.ToString() + " / " + _expSlider.maxValue.ToString();
    }

    public void OnUpdateCoin()
    {
        _coin.text = Managers.UserData.Coin.ToString();
    }

    public void OnUpdateEnergy()
    {
        _energy.text = Managers.UserData.Energy.ToString() + " / " + Managers.UserData.GetMax(nameof(UserData.Energy));
    }

    public void OnUpdateKey()
    {
        _key.text = Managers.UserData.Key.ToString() + " / " + Managers.UserData.GetMax(nameof(UserData.Key));
    }

}
