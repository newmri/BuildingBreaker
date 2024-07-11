using UnityCoreLibrary;
using UnityEngine;

public class GameScene : BaseScene
{
    public Vector3 _spawnPos { get; set; } = new Vector3(0.0f, -4.25f, 0.0f);

    private void Awake()
    {
        Init(CoreDefine.Scene.Game);
    }

    protected override void Init(CoreDefine.Scene scene)
    {
        base.Init(scene);

        Managers.Object.AddPlayer(_spawnPos);
        Managers.UI.ShowSceneUI<UIGameScene>();
    }

    public override void Clear()
    {
        Managers.Clear();
    }
}
