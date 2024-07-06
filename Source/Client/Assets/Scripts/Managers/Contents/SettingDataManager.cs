using System;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using Newtonsoft.Json;

[Serializable]
public class SettingData
{
    [JsonProperty(Order = 1)]
    public byte Language;
}


public class SettingDataManager : JsonDataManager<SettingData>
{
    public byte Language { get { return _data.Language; } set { _data.Language = value; OnUpdate(nameof(Language)); } }
}
