using System.Collections.Generic;
using UnityCoreLibrary;
using System;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using System.IO;

[Serializable]
public struct SettingData
{
    public byte Language;
}

public class SettingDataManager
{
    private string _path;
    private readonly string _fileName = "Setting";
    private string _fullPath;
    private bool _isDirty = false;

    private SettingData _settingData;
    public bool IsLoaded { get; private set; } = false;

    public byte Language { get { return _settingData.Language; } set { _settingData.Language = value; _isDirty = true; } }

    public void Load()
    {
        _path = Application.persistentDataPath;
        _fullPath = _path + _fileName;

        if (false == File.Exists(_fullPath))
            return;

        string data = File.ReadAllText(_fullPath);
        _settingData = JsonUtility.FromJson<SettingData>(data);

        IsLoaded = true;
    }

    public void Save()
    {
        if (false == _isDirty)
            return;

        string data = JsonUtility.ToJson(_settingData);
        File.WriteAllText(_fullPath, data);
    }
}
