using System;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;
public class JsonDataManager<T>
{
    private string _fileName;
    private string _path;
    private string _fullPath;

    private byte[] _key;
    private byte[] _iv;

    protected bool _isDirty = false;

    protected T _data;

    public bool IsLoaded { get; private set; } = false;

    public JsonDataManager()
    {

    }

    public void Load()
    {
        var name = typeof(T).Name;
        _key = Managers.Security.CreateKey(name);
        _iv = Managers.Security.CreateIV(name);

        _fileName = Managers.Security.Encrypt(name, _key, _iv);

        _path = Application.persistentDataPath;
        _fullPath = _path + _fileName;

        if (false == File.Exists(_fullPath))
            return;

        _data = JsonUtility.FromJson<T>(Managers.Security.Decrypt(File.ReadAllText(_fullPath), _key, _iv));

        IsLoaded = true;
    }

    public void Save()
    {
        if (false == _isDirty)
            return;

        File.WriteAllText(_fullPath, Managers.Security.Encrypt(JsonUtility.ToJson(_data), _key, _iv));
    }
}
