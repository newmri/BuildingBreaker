using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;


public enum MessagePopupType : byte
{
    NORMAL = 0,
    ERROR,
    MAX,
};

[Serializable]
public struct MessagePopupInfo
{
    public MessagePopupType Type;
    public Color BackGroundColor;
    public Color LineColor;
    public Color TextColor;
}

public class MessageDataManager
{
    private bool _isLoaded = false;
    private MessagePopupInfo[] _messagePopupInfoList;

    public void Load()
    {
        if (_isLoaded)
            return;

        LoadPopup();

        _isLoaded = true;
    }

    private void LoadPopup()
    {
        _messagePopupInfoList = new MessagePopupInfo[(int)MessagePopupType.MAX]
        {
             new MessagePopupInfo{Type= MessagePopupType.NORMAL,
                 BackGroundColor= new Color(0.1294118f, 0.09803922f, 0.1176471f, 1.0f),
                 LineColor= new Color(0.3921569f, 0.3058824f, 0.3882353f, 1.0f),
                 TextColor= new Color(0.8553459f, 0.8553459f, 0.8553459f, 1.0f)},

             new MessagePopupInfo{Type= MessagePopupType.ERROR,
                 BackGroundColor= new Color(0.227451f, 0.1333333f, 0.1882353f, 1.0f),
                 LineColor= new Color(0.8156863f, 0.227451f, 0.2235294f, 1.0f),
                 TextColor= new Color(0.9764706f, 0.4431373f, 0.4823529f, 1.0f)},
        };
    }

    public MessagePopupInfo GetMessagePopup(MessagePopupType type)
    {
        return _messagePopupInfoList[(int)type];
    }
}
