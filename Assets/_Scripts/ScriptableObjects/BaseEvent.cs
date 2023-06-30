using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BaseEvents", menuName = "ScriptableObjects/CreateBaseEvent", order = 1)]
    public class BaseEvent : ScriptableObject
    {
        private List<Action> _eventsToRaise;
        public void Subscribe(Action action)
        {
            _eventsToRaise.Add(action);
        }

        public void UnSubscribe(Action action)
        {
            _eventsToRaise?.Remove(action);
        }

        public void OnEventRaised()
        {
            foreach (Action action in _eventsToRaise)
            {
                action.Invoke();
            }
        }
    }
}