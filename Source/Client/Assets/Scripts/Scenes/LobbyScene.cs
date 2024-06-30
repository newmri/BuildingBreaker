using UnityCoreLibrary;
using UnityEngine;

public class LobbyScene : BaseScene
{
    private void Awake()
    {
        Init(CoreDefine.Scene.Lobby);
    }

    protected override void Init(CoreDefine.Scene scene)
    {
        base.Init(scene);

        Managers.StageData.Load();

        Managers.UI.ShowSceneUI<UILobbyScene>();
    }

    public override void Clear()
    {
        Managers.UI.Clear();
        CoreManagers.Clear();
    }
}
