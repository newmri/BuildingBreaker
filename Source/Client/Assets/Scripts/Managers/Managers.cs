using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성이 보장된다
    public static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다

    #region Contents
    SettingDataManager _settingData = new SettingDataManager();
    MessageDataManager _messageData = new MessageDataManager();
    UserDataManager _userData = new UserDataManager();
    StageDataManager _stageData = new StageDataManager();

    public static SettingDataManager SettingData { get { return Instance._settingData; } }
    public static MessageDataManager MessageData { get { return Instance._messageData; } }
    public static UserDataManager UserData { get { return Instance._userData; } }
    public static StageDataManager StageData { get { return Instance._stageData; } }

    #endregion

    #region Core
    SecurityManager _security = new SecurityManager();
    LangugaeManager _langugae = new LangugaeManager();
    UIManager _ui = new UIManager();

    public static SecurityManager Security { get { return Instance._security; } }
    public static LangugaeManager Langugae { get { return Instance._langugae; } }
    public static UIManager UI { get { return Instance._ui; } }

    #endregion

    void Start()
    {
        Init();
    }

    void Update()
    {

    }

    static void Init()
    {
        if (s_instance == null)
        {
			GameObject managersObject = GameObject.Find("@Managers");
            if (managersObject == null)
            {
                managersObject = new GameObject { name = "@Managers" };
                managersObject.AddComponent<Managers>();
            }

            DontDestroyOnLoad(managersObject);
            s_instance = managersObject.GetComponent<Managers>();

        }		
	}

    void OnApplicationQuit()
    {
        UserData.Save();
        SettingData.Save();
        StageData.Save();
    }

    public static void Clear()
    {
        UI.Clear();
        SettingData.Clear();
        UserData.Clear();
    }
}
