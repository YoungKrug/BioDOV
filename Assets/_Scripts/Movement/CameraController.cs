using System;
using _Scripts.Interface;
using _Scripts.ScriptableObjects;
using UnityEngine;

namespace _Scripts.Movement
{
    public class CameraController : MonoBehaviour
    {
        public Transform cameraObject;
        public BaseEventScriptableObject inputScriptableObject;
        private CameraMovement _cameraMovement;
        private CameraRotation _cameraRotation;
        private void Start()
        {
            _cameraMovement = new CameraMovement(inputScriptableObject, cameraObject);
            _cameraRotation = new CameraRotation(inputScriptableObject, cameraObject);
        }

    }
}