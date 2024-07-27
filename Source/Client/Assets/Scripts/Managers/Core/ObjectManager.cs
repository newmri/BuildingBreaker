using UnityCoreLibrary;
using UnityEngine;

public class ObjectManager
{
    public PlayerController Player { get; set; }
    public GroundObject PlayerGround { get; set; }
    public CameraController Camera { get; set; }
    public GameObject Stage { get; set; }

    private Vector3 _groundPosition;
    public Vector3 GroundPosition { get { return _groundPosition; } set { _groundPosition = value; } }

    public void Init()
    {
        Stage = GameObject.FindGameObjectWithTag("Stage");

        var ground = Util.FindChild(Stage, "Ground");
        _groundPosition = ground.transform.localPosition;
        _groundPosition.y += (ground.GetComponent<BoxCollider2D>().size.y / 2);

        Managers.Object.AddPlayer(Stage);
    }

    private void AddPlayer(GameObject parent)
    {
        var gameObject = CoreManagers.Obj.Add("Player", "Player", null, 1, parent.transform);
        Player = gameObject.GetOrAddComponent<PlayerController>();
        PlayerGround = gameObject.GetOrAddComponent<GroundObject>();
        Camera = UnityEngine.Camera.main.GetComponent<CameraController>();
        Camera.Offset = GroundPosition;
    }

    public void Clear()
    {
        
    }
}
