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
    public string NickName { get { return _data[0].NickName; } set { _data[0].NickName = value; OnUpdate(nameof(UserData.NickName)); } }
    public byte Level { get { return _data[0].Level; } set { if (value > GetMax(nameof(UserData.Level))) return; _data[0].Level = value; OnUpdate(nameof(UserData.Level)); } }
    public uint EXP { get { return _data[0].EXP; } set { _data[0].EXP = value; OnUpdate(nameof(UserData.EXP)); } }
    public uint Coin { get { return _data[0].Coin; } set { if (value > GetMax(nameof(UserData.Coin))) return;  _data[0].Coin = value; OnUpdate(nameof(UserData.Coin)); } }
    public ushort Energy { get { return _data[0].Energy; } set { if (value > GetMax(nameof(UserData.Energy))) return;  _data[0].Energy = value; OnUpdate(nameof(UserData.Energy)); } }
    public ushort Key { get { return _data[0].Key; } set { if (value > GetMax(nameof(UserData.Key))) return;  _data[0].Key = value; OnUpdate(nameof(UserData.Key)); } }

    private Dictionary<string, object> _defaultUserData;
    private Dictionary<string, int> _maxUserData;
    private Dictionary<int, Dictionary<string, object>> _levelUserData;

    public void Load()
    {
        if (IsLoaded)
            return;

        Load(1);

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
        return (int)_levelUserData[level][nameof(UserData.EXP)];
    }
}
