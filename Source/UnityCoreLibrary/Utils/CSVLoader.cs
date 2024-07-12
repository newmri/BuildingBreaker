using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace UnityCoreLibrary
{
    public class CSVLoader
    {
        string _split = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        string _lineSplit = @"\r\n|\n\r|\n|\r";
        char[] _trim = { '\"' };

        public void Load<T>(string path, out Dictionary<string, T> dataList)
        {
            dataList = new Dictionary<string, T>();
            TextAsset data = CoreManagers.Resource.Load<TextAsset>(path);

            var lines = Regex.Split(data.text, _lineSplit);

            if (lines.Length <= 1)
                return;

            var header = Regex.Split(lines[0], _split);
            for (var i = 1; i < lines.Length; i++)
            {
                var values = Regex.Split(lines[i], _split);
                if (values.Length == 0 || values[0] == "")
                    continue;

                for (var j = 0; j < header.Length && j < values.Length; j++)
                {
                    string value = values[j];

                    object finalvalue = Parse(value);
                    dataList[header[j]] = (T)finalvalue;
                }
            }
        }

        private object Parse(string value)
        {
            if (int.TryParse(value, out int intResult))
                return intResult;
            else if (float.TryParse(value, out float floatResult))
                return floatResult;
            else
                return value;
        }

        public void Load(string path, out List<Dictionary<string, object>> dataList)
        {
            dataList = new List<Dictionary<string, object>>();
            TextAsset data = CoreManagers.Resource.Load<TextAsset>(path);

            var lines = Regex.Split(data.text, _lineSplit);

            if (lines.Length <= 1)
                return;

            var header = Regex.Split(lines[0], _split);
            for (var i = 1; i < lines.Length; i++)
            {
                var values = Regex.Split(lines[i], _split);
                if (values.Length == 0 || values[0] == "")
                    continue;

                var entry = new Dictionary<string, object>();
                for (var j = 0; j < header.Length && j < values.Length; j++)
                {
                    string value = values[j];

                    object finalvalue = Parse(value);
                    entry[header[j]] = finalvalue;
                }
                dataList.Add(entry);
            }
        }

        public void Load(string key, string path, out Dictionary<int, Dictionary<string, object>> dataList)
        {
            List<Dictionary<string, object>> data;
            Load(path, out data);

            dataList = new Dictionary<int, Dictionary<string, object>>();

            foreach (var obj in data)
            {
                dataList.Add((int)obj[key], obj);
            }
        }

    }
}
