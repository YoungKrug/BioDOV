using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using UnityEngine;

namespace _Scripts.Movement
{
    public class CameraRotation : IInputReceivers
    {
        public KeyCode[] Keys { get; } = new KeyCode[] { KeyCode.Mouse0 };
        private const float SpeedH = 2.0f;
        private const float SpeedV = 2.0f;
        private readonly Transform _camera;
        private float _yaw = 0.0f;
        private float _pitch = 0.0f;
        public CameraRotation(BaseEventScriptableObject inputScriptableObject, Transform
             camera)
        {
            inputScriptableObject.OnEventRaised(this);
            _camera = camera;
        }
        private void Rotate() // Rotates the camera
        {
            _yaw += SpeedH * Input.GetAxis("Mouse X");
            _pitch -= SpeedV * Input.GetAxis("Mouse Y");
            _camera.eulerAngles = new Vector3(_pitch, _yaw, 0.0f);
        }
        public void ExecuteKey(KeyCode code)
        {
            Rotate();
        }
    }
}