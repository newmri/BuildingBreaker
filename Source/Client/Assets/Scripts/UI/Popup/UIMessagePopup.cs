using TMPro;
using UnityEngine.DedicatedServer;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class UIMessagePopup : UIPopup
{
    private MessagePopupType _type;
    private LocalizationInfo _text;
    private object[] _arguments = null;

    enum TextMeshProUGUIs
    {
        Text,
    }

    enum Images
    {
        Background,
        Line,
    }

    public override void Init()
    {
        base.Init();

        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));
        Bind<Image>(typeof(Images));

        var text = this.GetTextMesh((int)TextMeshProUGUIs.Text);

        var localizedString = text.GetComponent<LocalizeStringEvent>().StringReference;

        if(null != _arguments)
            localizedString.Arguments = _arguments;

        localizedString.SetReference(_text.Table, _text.Key);

        var info = Managers.MessageData.GetMessagePopup(_type);

        GetImage((int)Images.Background).color = info.BackGroundColor;
        GetImage((int)Images.Line).color = info.LineColor;
        text.color = info.TextColor;
    }

    public void SetText(MessagePopupType type, LocalizationInfo text, object[] arguments = null)
    {
        _type = type;
        _text = text;
        _arguments = arguments;
    }
}
