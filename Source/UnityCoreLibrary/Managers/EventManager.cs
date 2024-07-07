using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace UnityCoreLibrary.Managers
{
    public class EventManager
    {
        public delegate void OnEvent(int index);

        private Dictionary<string, List<OnEvent>> _listners = new Dictionary<string, List<OnEvent>>();

        public void AddListener(string eventType, OnEvent listener)
        {
            List<OnEvent> listnerList = null;
            if(_listners.TryGetValue(eventType, out listnerList))
            {
                listnerList.Add(listener);
                listener(-1);
                return;
            }

            listnerList = new List<OnEvent>();
            listnerList.Add(listener);

            _listners.Add(eventType, listnerList);

            listener(-1);
        }

        public void RemoveEvent(string eventType) => _listners.Remove(eventType);

        public void PostNotification(string eventType, int index)
        {
            List<OnEvent> listnerList = null;
            if (false == _listners.TryGetValue(eventType, out listnerList))
                return;

            foreach (var listener in listnerList)
                listener(index);
        }

        public void Clear()
        {
            _listners.Clear();
        }
    }
}
