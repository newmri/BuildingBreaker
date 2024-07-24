using UnityCoreLibrary;
using UnityEngine;

public class ObjectManager
{
    public PlayerController Player { get; set; }
    public CameraController Camera { get; set; }

    private Vector3 _groundPosition;
    public Vector3 GroundPosition { get { return _groundPosition; } set { _groundPosition = value; } }

    public void Init()
    {
        var stage = GameObject.FindGameObjectWithTag("Stage");

        var ground = Util.FindChild(stage, "Ground");
        _groundPosition = ground.transform.localPosition;
        _groundPosition.y += (ground.GetComponent<BoxCollider2D>().size.y / 2);

        Managers.Object.AddPlayer(stage);
    }

    private void AddPlayer(GameObject parent)
    {
        var gameObject = CoreManagers.Obj.Add("Player", "Player", null, 1, parent.transform);
        Player = gameObject.GetOrAddComponent<PlayerController>();
        Camera = UnityEngine.Camera.main.GetComponent<CameraController>();
        Camera.Offset = GroundPosition;
    }

    public void Clear()
    {
        
    }
}
