using System;
using UnityCoreLibrary;
using UnityEngine;
using UnityEngine.UI;
using UnityCoreLibrary.UI;

public class UICheckButton : UIAwakeBase
{
    private Image _checkIcon = null;

    public int Index { get; set; }

    public override void Init()
    {
        _checkIcon = Util.FindChild<Image>(gameObject);
    }

    public void OnSelected()
    {
        _checkIcon.gameObject.SetActive(true);
    }

    public void OnUnSelected()
    {
        _checkIcon.gameObject.SetActive(false);
    }
}
