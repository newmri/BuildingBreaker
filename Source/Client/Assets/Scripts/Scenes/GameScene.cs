using UnityCoreLibrary;

public class GameScene : BaseScene
{
    private void Awake()
    {
        Init(CoreDefine.Scene.Game);
    }

    protected override void Init(CoreDefine.Scene scene)
    {
        base.Init(scene);

        Managers.SkillData.Load();
        Managers.UI.ShowSceneUI<UIGameScene>();

        Managers.Object.Init();
    }

    public override void Clear()
    {
        Managers.Clear();
    }
}
