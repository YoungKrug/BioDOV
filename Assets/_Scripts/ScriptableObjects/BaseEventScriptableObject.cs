using System;
using System.Collections.Generic;
using _Scripts.Interface;
using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BaseEvents", menuName = "ScriptableObjects/CreateBaseEvent", order = 1)]
    public class BaseEventScriptableObject : ScriptableObject
    {
        private List<IEventReactor> _eventsToRaise = new List<IEventReactor>();
        //Objects that are called when the OnEventRaised Function is called
        public void Subscribe(IEventReactor eventReactor)
        {
            _eventsToRaise.Add(eventReactor);
        }
        //Remove objects to be called from the OnEventRaised Function
        public void UnSubscribe(IEventReactor eventReactor)
        {
            _eventsToRaise?.Remove(eventReactor);
        }
        //Raises the events, and calls the event function
        public void OnEventRaised(object objectToSend)
        {
            foreach (IEventReactor eventReactor in _eventsToRaise)
            {
                eventReactor.Execute(objectToSend);
            }
        }
    }
}