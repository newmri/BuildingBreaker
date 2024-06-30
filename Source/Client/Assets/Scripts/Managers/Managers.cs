﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성이 보장된다
    public static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다

    #region Contents
    StageDataManager _stageData = new StageDataManager();

    public static StageDataManager StageData { get { return Instance._stageData; } }

    #endregion

    #region Core
    UIManager _ui = new UIManager();

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
    }

    public static void Clear()
    {
        UI.Clear();
    }
}
