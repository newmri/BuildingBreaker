using System;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

[Serializable]
public struct UserData
{
    public string Name;
}


public class UserDataManager : JsonDataManager<UserData>
{
    public string Name { get { return _data.Name; } set { _data.Name = value; _isDirty = true; } }
}
