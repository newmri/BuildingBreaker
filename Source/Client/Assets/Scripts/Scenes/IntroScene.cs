using System.Collections;
using UnityCoreLibrary;
using UnityEngine.Localization.Settings;


public class IntroScene : BaseScene
{
    private void Awake()
    {
        Init(CoreDefine.Scene.Intro);
    }

    protected override void Init(CoreDefine.Scene scene)
    { 
        base.Init(scene);

        Managers.SettingData.Load();
        Managers.Langugae.Init();

        Managers.MessageData.Load();
        Managers.UserData.Load();

        CoreManagers.Scene.LoadScene(CoreDefine.Scene.Title);
    }

    public override void Clear()
    {
        Managers.Clear();
    }
}

