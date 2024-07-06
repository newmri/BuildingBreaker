using System;
using UnityCoreLibrary;
using Newtonsoft.Json;

[Serializable]
public class UserData
{
    [JsonProperty(Order = 1)]
    public string NickName;

    [JsonProperty(Order = 2)]
    public byte Level = 1;

    [JsonProperty(Order = 3)]
    public byte EXP = 0;
}


public class UserDataManager : JsonDataManager<UserData>
{
    public string NickName { get { return _data.NickName; } set { _data.NickName = value; OnUpdate(nameof(NickName)); } }
    public byte Level { get { return _data.Level; } set { _data.Level = value; OnUpdate(nameof(Level)); } }
    public byte EXP { get { return _data.EXP; } set { _data.EXP = value; OnUpdate(nameof(EXP)); } }
}
