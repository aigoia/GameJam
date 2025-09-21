using UnityEditor;
using UnityEngine;

namespace Script.Common
{
    public class CameraController : MonoBehaviour
    {
        public bool isBounce = true;
        
        float MoveSpeed => 10f;
        float SprintMultiplier => 2f;
        float MouseSensitivity => 60f;
        float ScrollSpeed => 5f;  
        float PositionSmoothTime => 0.05f;
        float RotationSmoothFactor => 0.2f;
        float ZoomSmoothTime => 0.1f;
        float IdleSmoothTime => 0.3f;
        float IdleMoveSmoothTime => 0.2f;
        float IdleAmount => 1f;

        float rotationX = 0f;
        float rotationY = 0f;

        Vector3 currentVelocity;
        Vector3 targetPosition;
        Quaternion currentRotation;
        Quaternion targetRotation;

        float zoomTarget = 0f;
        float currentZoomDistance = 0f;
        float zoomVelocity = 0f;

        float idleYOffset = 0f;
        float idleVelocity = 0f;
        
        void Start()
        {
            targetPosition = transform.position;
            targetRotation = transform.rotation;
            currentRotation = transform.rotation;
            currentZoomDistance = 0f;
            // Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            float speed = MoveSpeed * (Input.GetKey(KeyCode.LeftShift) ? SprintMultiplier : 1f);
            Vector3 inputMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            Vector3 moveDelta = transform.TransformDirection(inputMove) * (speed * Time.deltaTime);
            targetPosition += moveDelta;

            float scroll = Input.mouseScrollDelta.y;
            zoomTarget += scroll * ScrollSpeed;
            currentZoomDistance = Mathf.SmoothDamp(currentZoomDistance, zoomTarget, ref zoomVelocity, ZoomSmoothTime);

            float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

            rotationX -= mouseY;
            rotationY += mouseX;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);
            
            Vector3 desiredPosition = targetPosition + currentRotation * Vector3.forward * currentZoomDistance;

            if (isBounce) {
                targetRotation = Quaternion.Euler(rotationX, rotationY, 0f);
                currentRotation = Quaternion.Slerp(currentRotation, targetRotation, RotationSmoothFactor);
            
                float targetIdleYOffset = (Mathf.PerlinNoise(0f, Time.time * IdleMoveSmoothTime) - IdleMoveSmoothTime) * IdleAmount;
                idleYOffset = Mathf.SmoothDamp(idleYOffset, targetIdleYOffset, ref idleVelocity, IdleSmoothTime);
            
                desiredPosition.y += idleYOffset;
            }
            
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, PositionSmoothTime);
            transform.rotation = currentRotation;
        }
    }
}
