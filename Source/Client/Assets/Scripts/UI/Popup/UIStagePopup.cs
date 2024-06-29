using System.Collections.Generic;
using UnityCoreLibrary;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class UIStagePopup : UIPopup
{
    enum GameObjects
    {
        Page_Scroll,
        PageNavi,
    }

    enum Buttons
    {
        Prev_Button,
        Next_Button,
    }

    private SwipeController _swipeController;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));

        _swipeController = GetObject((int)GameObjects.Page_Scroll).GetComponent<SwipeController>();
        _swipeController.SetNavi(Util.FindAllChildrens<Image>(GetObject((int)GameObjects.PageNavi)));

        GetButton((int)Buttons.Prev_Button).gameObject.BindEvent(OnClickPrevButton);
        GetButton((int)Buttons.Next_Button).gameObject.BindEvent(OnClickNextButton);
    }

    public void OnClickPrevButton(PointerEventData evt)
    {
        _swipeController.Previous();
    }

    public void OnClickNextButton(PointerEventData evt)
    {
        _swipeController.Next();
    }
}
