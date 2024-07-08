using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization.Components;

public class UINickNameInputPopup : UIPopup
{
    enum TextMeshProUGUIs
    {
        Title_Text,
        Placeholder_Text,
        Info_Text
    }

    enum TMP_InputFields
    {
        NickName_Input,
    }

    enum Buttons
    {
        OK_Button,
    }

    private LocalizationInfo _title;
    private LocalizationInfo _placeholder;
    private LocalizationInfo _info;

    private readonly int _nickNameMinLen = 1;
    private readonly int _nickNameMaxLen = 10;

    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
        Bind<TMP_InputField>(typeof(TMP_InputFields));
        Bind<Button>(typeof(Buttons));

        this.GetTextMesh((int)TextMeshProUGUIs.Title_Text).GetComponent<LocalizeStringEvent>().StringReference.SetReference(_title.Table, _title.Key);
        this.GetTextMesh((int)TextMeshProUGUIs.Placeholder_Text).GetComponent<LocalizeStringEvent>().StringReference.SetReference(_placeholder.Table, _placeholder.Key);
        this.GetTextMesh((int)TextMeshProUGUIs.Info_Text).GetComponent<LocalizeStringEvent>().StringReference.SetReference(_info.Table, _info.Key);

        this.GetInputText((int)TMP_InputFields.NickName_Input).characterLimit = _nickNameMaxLen;

        GetButton((int)Buttons.OK_Button).gameObject.BindEvent(OnClickOKButton);
    }

    public void SetText(LocalizationInfo title, LocalizationInfo placeholder, LocalizationInfo info)
    {
        _title = title;
        _placeholder = placeholder;
        _info = info;
    }

    public void OnClickOKButton(PointerEventData evt)
    {
        if (false == IsValidNickNameLen())
        {
            var popup = Managers.UI.ShowPopupUI<UIMessagePopup>();
            popup.SetText(MessagePopupType.ERROR, new LocalizationInfo("Message", "NickNameLenError"), new object[] { _nickNameMinLen, _nickNameMaxLen });
            return;
        }

        Managers.UserData.NickName = this.GetInputText((int)TMP_InputFields.NickName_Input).text;
        Managers.UI.ClosePopupUI();
    }

    private bool IsValidNickNameLen()
    {
        var nickNameInput = this.GetInputText((int)TMP_InputFields.NickName_Input);

        bool isValid = (nickNameInput.text.Length >= _nickNameMinLen &&
                        nickNameInput.text.Length <= _nickNameMaxLen
                        );

        return isValid;
    }
}
