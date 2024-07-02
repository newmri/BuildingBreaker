using System;
using UnityEngine;
using System.IO;
using System.Linq;
public class JsonDataManager<T>
{
    private readonly string _fileName;
    private string _path;
    private string _fullPath;

    private readonly byte _key;
    protected bool _isDirty = false;

    protected T _data;

    public bool IsLoaded { get; private set; } = false;

    public JsonDataManager(string fileName)
    {
        _fileName = fileName;
        _key = (byte)_fileName.First();
    }

    public void Load()
    {
        _path = Application.persistentDataPath;
        _fullPath = _path + _fileName;

        if (false == File.Exists(_fullPath))
            return;

        _data = JsonUtility.FromJson<T>(Managers.Security.Decrypt(_key, File.ReadAllText(_fullPath)));

        IsLoaded = true;
    }

    public void Save()
    {
        if (false == _isDirty)
            return;

        File.WriteAllText(_fullPath, Managers.Security.Encrypt(_key, JsonUtility.ToJson(_data)));
    }
}
