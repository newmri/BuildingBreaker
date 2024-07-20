using UnityCoreLibrary;
using UnityCoreLibrary.Managers;
using UnityEngine;

public class ObjectManager
{
    public PlayerController Player { get; set; }
    public CameraController Camera { get; set; }

    public void AddPlayer(Vector3 spawnPos)
    {
        GameObject gameObject = CoreManagers.Obj.Add("Player", "Player", spawnPos);
        Player = gameObject.GetOrAddComponent<PlayerController>();
        Camera = UnityEngine.Camera.main.GetComponent<CameraController>();
        Camera.Offset = spawnPos;
    }

    public void Clear()
    {
        
    }
}
