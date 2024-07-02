using System;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

[Serializable]
public struct SettingData
{
    public byte Language;
}


public class SettingDataManager : JsonDataManager<SettingData>
{
    public byte Language { get { return _data.Language; } set { _data.Language = value; _isDirty = true; } }
}
