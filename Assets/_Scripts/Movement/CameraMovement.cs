using System;
using UnityEngine;

namespace _Scripts.Movement
{
    public class CameraMovement : MonoBehaviour
    {
        public Transform cameraObject;
        private readonly float speed = 5f;
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
                case KeyCode.S:
                    cameraObject.Translate(new 
                        Vector3(speed * Time.deltaTime, 0));
                    break;
                case KeyCode.D:
                    cameraObject.Translate(new 
                        Vector3(0, 0, -speed * Time.deltaTime));
                    break;
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.D))
            {
                MoveCamera(KeyCode.D);
            }
        }
    }
}