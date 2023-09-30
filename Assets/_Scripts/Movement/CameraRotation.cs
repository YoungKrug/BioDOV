using UnityEngine;

namespace _Scripts.Movement
{
    public class CameraRotation : MonoBehaviour
    {
        
        public float speedH = 2.0f;
        public float speedV = 2.0f;

        private float yaw = 0.0f;
        private float pitch = 0.0f;

        void Update()
        {
            if (!Input.GetMouseButton(0)) return;
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
}