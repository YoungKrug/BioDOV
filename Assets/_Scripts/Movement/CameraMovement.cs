using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using UnityEngine;

namespace _Scripts.Movement
{
    public class CameraMovement : IInputReceivers
    {
        public KeyCode[] Keys { get; } = new KeyCode[] {KeyCode.A, KeyCode.D, KeyCode.S, KeyCode.W};
        private readonly Transform _camera;
        private const float Speed = 5f;

        public CameraMovement(BaseEventScriptableObject inputEventScriptableObject, Transform camera)
        {
            inputEventScriptableObject.OnEventRaised(this);
            _camera = camera;
        }

        private void MoveCamera(KeyCode keyCode, Transform cameraObject)
        {
            switch (keyCode)
            {
                case KeyCode.A:
                    cameraObject.Translate(new 
                        Vector3(-Speed * Time.deltaTime, 0));
                    break;
                case KeyCode.W:
                    cameraObject.Translate(new 
                        Vector3(0, 0, Speed * Time.deltaTime));
                    break;
                case KeyCode.D:
                    cameraObject.Translate(new 
                        Vector3(Speed * Time.deltaTime, 0));
                    break;
                case KeyCode.S:
                    cameraObject.Translate(new 
                        Vector3(0, 0, -Speed * Time.deltaTime));
                    break;
            }
        }
        
        public void ExecuteKey(KeyCode code)
        {
            MoveCamera(code, _camera);
        }
    }
}