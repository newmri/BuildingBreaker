using System.Collections.Generic;
using UnityCoreLibrary;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using NUnit.Framework;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using System.Collections;

public class UILanguagePopup : UIPopup
{
    enum Buttons
    {
        Close_Button,
    }

    enum GameObjects
    {
        LanguageList
    }

    private bool _isChanging = false;

    private List<UICheckIcon> _checkIconList = new List<UICheckIcon>();

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        GetButton((int)Buttons.Close_Button).gameObject.BindEvent(OnClickCloseButton);

        List<Button> checkButtonList = Util.FindAllChildrens<Button>(GetObject((int)GameObjects.LanguageList));

       for(int i = 0; i < checkButtonList.Count; ++i)
        {
            checkButtonList[i].gameObject.BindEvent(OnClickLanguageButton);

            var uiCheckButton = checkButtonList[i].GetComponent<UICheckIcon>();
            uiCheckButton.Index = i;
            _checkIconList.Add(uiCheckButton);

            if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[i])
                uiCheckButton.OnSelected();
        }
    }

    public void OnClickCloseButton(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }

    public void OnClickLanguageButton(PointerEventData evt)
    {
        foreach (var icon in _checkIconList)
            icon.OnUnSelected();

        var checkButton = evt.selectedObject.GetComponent<UICheckIcon>();
        checkButton.OnSelected();

        ChangeLocale(checkButton.Index);
    }

    public void ChangeLocale(int index)
    {
        if (_isChanging)
            return;

        StartCoroutine(LocaleChange(index));
    }

    IEnumerator LocaleChange(int index)
    {
        _isChanging = true;

        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        _isChanging = false;
    }
}
