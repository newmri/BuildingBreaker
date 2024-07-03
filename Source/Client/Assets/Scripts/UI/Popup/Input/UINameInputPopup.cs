using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Localization.Components;

public class UINameInputPopup : UIPopup
{
    enum TextMeshProUGUIs
    {
        Title_Text,
        Placeholder_Text,
        Info_Text
    }

    enum TMP_InputFields
    {
        Name_Input,
    }

    private LocalizationInfo _title;
    private LocalizationInfo _placeholder;
    private LocalizationInfo _info;

    private readonly int _nameMaxLen = 10;

    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
        Bind<TMP_InputField>(typeof(TMP_InputFields));

        this.GetTextMesh((int)TextMeshProUGUIs.Title_Text).GetComponent<LocalizeStringEvent>().StringReference.SetReference(_title.Table, _title.Key);
        this.GetTextMesh((int)TextMeshProUGUIs.Placeholder_Text).GetComponent<LocalizeStringEvent>().StringReference.SetReference(_placeholder.Table, _placeholder.Key);
        this.GetTextMesh((int)TextMeshProUGUIs.Info_Text).GetComponent<LocalizeStringEvent>().StringReference.SetReference(_info.Table, _info.Key);

        this.GetInputText((int)TMP_InputFields.Name_Input).characterLimit = _nameMaxLen;
    }

    public void SetText(LocalizationInfo title, LocalizationInfo placeholder, LocalizationInfo info)
    {
        _title = title;
        _placeholder = placeholder;
        _info = info;
    }

    public void OnClickOKButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
