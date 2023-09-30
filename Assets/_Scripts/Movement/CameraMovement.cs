using System;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using UnityEngine;

namespace _Scripts.Movement
{
    
    //TODO Create a camera rotation system
    public class CameraMovement : MonoBehaviour, IInputReceivers
    {
        public KeyCode[] Keys { get; } = new KeyCode[] {KeyCode.A, KeyCode.D, KeyCode.S, KeyCode.W};
        public Transform cameraObject;
        private readonly float speed = 5f;
        public BaseEventScriptableObject inputScriptableObject;
        private void MoveCamera(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.A:
                    cameraObject.Translate(new 
                        Vector3(-speed * Time.deltaTime, 0));
                    break;
                case KeyCode.W:
                    cameraObject.Translate(new 
                        Vector3(0, 0, speed * Time.deltaTime));
                    break;
                case KeyCode.D:
                    cameraObject.Translate(new 
                        Vector3(speed * Time.deltaTime, 0));
                    break;
                case KeyCode.S:
                    cameraObject.Translate(new 
                        Vector3(0, 0, -speed * Time.deltaTime));
                    break;
            }
        }
        void Start()
        {
            inputScriptableObject.OnEventRaised(this);
        }
        public void ExecuteKey(KeyCode key)
        {
            MoveCamera(key);
        }
    }
}