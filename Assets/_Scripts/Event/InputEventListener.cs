using System.Collections.Generic;
using System.Linq;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using Accord;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace _Scripts.Event
{
    public class InputEventListener : MonoBehaviour, IEventReactor
    {
        public BaseEventScriptableObject inputEventScriptableObject;
        private readonly List<IInputReceivers> _receiversList = new List<IInputReceivers>();

        private void Awake()
        {
            inputEventScriptableObject.Subscribe(this);
        }
        private void Update()
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(keyCode))
                {
                    CheckForInputEvent(keyCode);
                }
            }
        }
        private void CheckForInputEvent(KeyCode key)
        {
            foreach (var receiver in _receiversList)
            {
                if (receiver.Keys.Contains(key))
                {
                    receiver.ExecuteKey(key);
                }
            }
        }
        public void Execute(object obj)
        {
            var inputReceivers = (IInputReceivers)obj;
            _receiversList.Add(inputReceivers);
            Debug.Log(obj.ToString());
        }
    }
}