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
    public byte Language { get { return _data[0].Language; } set { _data[0].Language = value; OnUpdate(nameof(Language)); } }

    public void Load()
    {
        Load(1);
    }
}
