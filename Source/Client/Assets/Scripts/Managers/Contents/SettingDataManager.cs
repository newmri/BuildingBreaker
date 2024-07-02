using System;
using UnityEngine;
using System.IO;

[Serializable]
public struct SettingData
{
    public byte Language;
}


public class SettingDataManager : JsonDataManager<SettingData>
{
    public SettingDataManager() : base("Setting")
    {

    }

    public byte Language { get { return _data.Language; } set { _data.Language = value; _isDirty = true; } }
}
