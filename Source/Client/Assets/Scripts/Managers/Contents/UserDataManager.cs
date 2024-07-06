using System;
using UnityCoreLibrary;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEditor.SceneManagement;


[Serializable]
public class UserData
{
    [JsonProperty(Order = 1)]
    public string NickName;

    [JsonProperty(Order = 2)]
    public byte Level;

    [JsonProperty(Order = 3)]
    public uint EXP;

    [JsonProperty(Order = 4)]
    public uint Coin;

    [JsonProperty(Order = 5)]
    public ushort Energy;

    [JsonProperty(Order = 6)]
    public ushort Key;
}

public class UserDataManager : JsonDataManager<UserData>
{
    public string NickName { get { return _data.NickName; } set { _data.NickName = value; OnUpdate(nameof(NickName)); } }
    public byte Level { get { return _data.Level; } set { if (value > GetMax(nameof(Level))) return; _data.Level = value; OnUpdate(nameof(Level)); } }
    public uint EXP { get { return _data.EXP; } set { _data.EXP = value; OnUpdate(nameof(EXP)); } }
    public uint Coin { get { return _data.Coin; } set { if (value > GetMax(nameof(Coin))) return;  _data.Coin = value; OnUpdate(nameof(Coin)); } }
    public ushort Energy { get { return _data.Energy; } set { if (value > GetMax(nameof(Energy))) return;  _data.Energy = value; OnUpdate(nameof(Energy)); } }
    public ushort Key { get { return _data.Key; } set { if (value > GetMax(nameof(Key))) return;  _data.Key = value; OnUpdate(nameof(Key)); } }

    Dictionary<string, object> _defaultUserData;
    Dictionary<string, int> _maxUserData;
    Dictionary<int, Dictionary<string, object>> _levelUserData;

    public override void Load()
    {
        base.Load();

        CoreManagers.Data.LoadCSV("Data/UserData/DefaultUserData", out _defaultUserData);
        CoreManagers.Data.LoadCSV("Data/UserData/MaxUserData", out _maxUserData);
        CoreManagers.Data.LoadCSV(nameof(Level), "Data/UserData/LevelUserData", out _levelUserData);

        if (false == IsLoaded)
        {
            Level = (byte)(int)_defaultUserData[nameof(Level)];
            Coin = (uint)(int)_defaultUserData[nameof(Coin)];
            Energy = (ushort)(int)_defaultUserData[nameof(Energy)];
            Key = (ushort)(int)_defaultUserData[nameof(Key)];
        }
    }

    public int GetMax(string name)
    {
        return _maxUserData[name];
    }

    public int GetMaxExp(byte level)
    {
        return (int)_levelUserData[level][nameof(EXP)];
    }
}
