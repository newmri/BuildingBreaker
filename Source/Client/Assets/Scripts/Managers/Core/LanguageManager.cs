using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;
using UnityCoreLibrary;
using UnityEngine.Localization;


public class LangugaeManager
{

    public void Init()
    {
        if (!LocalizationSettings.InitializationOperation.IsDone)
            LocalizationSettings.InitializationOperation.WaitForCompletion();

        int index = 0;
        if (Managers.SettingData.IsLoaded)
            index = Managers.SettingData.Language;
        else
        {
            var language = Application.systemLanguage;
            var languageName = language.ToString();

            var localeList = LocalizationSettings.AvailableLocales.Locales;

            index = FindLocaleIndex(languageName, localeList);
            if (-1 == index)
                index = FindLocaleIndex("English", localeList);
        }

        ChangeLocale(index);
    }

    private int FindLocaleIndex(string name, List<Locale> localeList)
    {
        for (int i = 0; i < localeList.Count; ++i)
        {
            if (localeList[i].LocaleName.Contains(name))
                return i;
        }

        return -1;
    }

    public void ChangeLocale(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        Managers.SettingData.Language = (byte)index;
    }
}
