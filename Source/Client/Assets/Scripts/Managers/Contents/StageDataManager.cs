using System.Collections.Generic;
using UnityCoreLibrary;
using System;
using UnityEditor.Localization.Plugins.XLIFF.V12;

[Serializable]
public struct Stage
{
    public int StageID;
}

public class StageDataManager
{
    private bool _isLoaded = false;

    Dictionary<int, Dictionary<string, object>> _stageList;

    public void Load()
    {
        if (_isLoaded)
            return;

        _isLoaded = true;

        CoreManagers.Data.LoadCSV("StageID", "Data/Stage/Stage", out _stageList);
    }

    public int GetStageCount()
    {
        return _stageList.Count;
    }

    public void GetStage(int stageID, ref Stage stage)
    {
        stage.StageID = (int)_stageList[stageID]["StageID"];
    }
}
