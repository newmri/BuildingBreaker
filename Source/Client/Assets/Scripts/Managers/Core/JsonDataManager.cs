using System;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text;
using UnityCoreLibrary.Managers;
using static UnityCoreLibrary.Managers.EventManager;
using NUnit.Framework;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.data;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.data = array;
        return JsonUtility.ToJson(wrapper);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] data;
    }
}

public class JsonDataManager<T> where T : new()
{
    private string _fileName;
    private string _path;
    private string _fullPath;

    private byte[] _key;
    private byte[] _iv;

    private bool _isDirty = false;

    protected T[] _data;

    public bool IsLoaded { get; private set; } = false;

    private EventManager _eventManager = new EventManager();

    public void Load(int count)
    {
        var name = typeof(T).Name;
        _key = Managers.Security.CreateKey(name);
        _iv = Managers.Security.CreateIV(name);

        _fileName = Managers.Security.Encrypt(name, _key, _iv);

        _path = Application.persistentDataPath;
        _fullPath = _path + "/" + _fileName;

        if (false == File.Exists(_fullPath))
        {
            _data = new T[count];
            for(int i = 0; i < count; ++i)
                _data[i] = new T();

            return;
        }

        _data = JsonHelper.FromJson<T>(Managers.Security.Decrypt(File.ReadAllText(_fullPath), _key, _iv));

        IsLoaded = true;
    }

    public void Save()
    {
        if (false == _isDirty)
            return;

        File.WriteAllText(_fullPath, Managers.Security.Encrypt(JsonHelper.ToJson(_data), _key, _iv));
    }

    public void AddListener(string eventType, OnEvent listener)
    {
        _eventManager.AddListener(eventType, listener);
    }

    public void OnUpdate(string eventType, int index = 0)
    {
        _eventManager.PostNotification(eventType, index);
        _isDirty = true;
    }

    public void Clear()
    {
        _eventManager.Clear();
    }
}
