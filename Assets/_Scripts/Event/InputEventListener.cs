using System.Collections.Generic;
using System.Linq;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace _Scripts.Event
{
    public class InputEventListener : MonoBehaviour, IEventReactor
    {
        public BaseEventScriptableObject inputEventScriptableObject;
        private List<IInputReceivers> _receiversList;
        private IEventReactor _eventReactorImplementation;

        void Update()
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    CheckForInputEvent(keyCode);
                }
            }
        }
        private void CheckForInputEvent(KeyCode key)
        {
            foreach (var receiver in _receiversList)
            {
                receiver.ExecuteKey();
            }
        }
        public void Execute(object obj)
        {
            var inputReceivers = (IInputReceivers)obj;
            _receiversList.Add(inputReceivers);
        }
    }
}