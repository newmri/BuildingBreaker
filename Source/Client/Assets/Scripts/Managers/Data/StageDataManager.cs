using System.Collections.Generic;
using UnityCoreLibrary;
using System;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using Newtonsoft.Json;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;
using UnityEditor.SceneManagement;
using UnityEngine.Diagnostics;

[Serializable]
public class StageData
{
    [JsonProperty(Order = 1)]
    public int StageID;

    [JsonProperty(Order = 2)]
    public bool IsOpen = false;

    [JsonProperty(Order = 3)]
    public byte StartCount = 0;
}

public class StageDataManager : JsonDataManager<StageData>
{
    public StageData[] GetStageData() { return _data; }
    public StageData GetStageData(int index) { return _data[index]; }
    public bool IsOpen(int stageID) { return _data[stageID].IsOpen; }
    public void SetOpen(int stageID, bool isOpen) { _data[stageID].IsOpen = isOpen; OnUpdate(nameof(StageData.IsOpen), stageID); }
    public byte GetStarCount(int stageID) { return _data[stageID].StartCount; }
    public void SetStarCount(int stageID, byte startCount) { _data[stageID].StartCount = startCount; OnUpdate(nameof(StageData.StartCount), stageID); }
    public byte GetMaxStarCount(int stageID) { return (byte)(int)_stageList[stageID]["MaxStar"]; }
    public int CurrentStageID { get; set; } = -1;

    private Dictionary<int, Dictionary<string, object>> _stageList;

    public void Load()
    {
        if (IsLoaded)
            return;

        CoreManagers.Data.LoadCSV("StageID", "Data/Stage/Stage", out _stageList);

        var stageCount = GetStageCount();

        Load(stageCount);

        if (false == IsLoaded)
        {
            for (int i = 0; i < stageCount; ++i)
            {
                _data[i].StageID = i;
                SetOpen(i, Convert.ToBoolean((int)_stageList[i][nameof(StageData.IsOpen)]));
            }

            return;
        }

        if (_data.Length < stageCount)
        {
            var originalLen = Util.Expand(ref _data, stageCount);
            for (int i = originalLen; i < stageCount; ++i)
            {
                _data[i] = new StageData();
                _data[i].StageID = i;
                SetOpen(i, Convert.ToBoolean((int)_stageList[i][nameof(StageData.IsOpen)]));
            }
        }
        else if (_data.Length > stageCount)
            Util.Shirink(ref _data, stageCount);
    }

    public int GetStageCount()
    {
        return _stageList.Count;
    }
}