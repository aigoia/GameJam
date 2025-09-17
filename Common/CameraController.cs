using UnityEditor;
using UnityEngine;

namespace Script.Common
{
    public class CameraController : MonoBehaviour
    {
        public bool isBounce = true;
        
        float MoveSpeed => 10f;
        float SprintMultiplier => 2f;
        float MouseSensitivity => 200f;
        float ScrollSpeed => 5f;  
        float PositionSmoothTime => 0.05f;
        float RotationSmoothFactor => 0.2f;
        float ZoomSmoothTime => 0.1f;
        float IdleSmoothTime => 0.3f;
        float IdleMoveSmoothTime => 0.2f;
        float IdleAmount => 1f;

        float _rotationX = 0f;
        float _rotationY = 0f;

        Vector3 _currentVelocity;
        Vector3 _targetPosition;
        Quaternion _currentRotation;
        Quaternion _targetRotation;

        float _zoomTarget = 0f;
        float _currentZoomDistance = 0f;
        float _zoomVelocity = 0f;

        float _idleYOffset = 0f;
        float _idleVelocity = 0f;
        
        void Start()
        {
            _targetPosition = transform.position;
            _targetRotation = transform.rotation;
            _currentRotation = transform.rotation;
            _currentZoomDistance = 0f;
            // Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            float speed = MoveSpeed * (Input.GetKey(KeyCode.LeftShift) ? SprintMultiplier : 1f);
            Vector3 inputMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            Vector3 moveDelta = transform.TransformDirection(inputMove) * (speed * Time.deltaTime);
            _targetPosition += moveDelta;

            float scroll = Input.mouseScrollDelta.y;
            _zoomTarget += scroll * ScrollSpeed;
            _currentZoomDistance = Mathf.SmoothDamp(_currentZoomDistance, _zoomTarget, ref _zoomVelocity, ZoomSmoothTime);

            float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

            _rotationX -= mouseY;
            _rotationY += mouseX;
            _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
            
            Vector3 desiredPosition = _targetPosition + _currentRotation * Vector3.forward * _currentZoomDistance;

            if (isBounce) {
                _targetRotation = Quaternion.Euler(_rotationX, _rotationY, 0f);
                _currentRotation = Quaternion.Slerp(_currentRotation, _targetRotation, RotationSmoothFactor);
            
                float targetIdleYOffset = (Mathf.PerlinNoise(0f, Time.time * IdleMoveSmoothTime) - IdleMoveSmoothTime) * IdleAmount;
                _idleYOffset = Mathf.SmoothDamp(_idleYOffset, targetIdleYOffset, ref _idleVelocity, IdleSmoothTime);
            
                desiredPosition.y += _idleYOffset;
            }
            
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _currentVelocity, PositionSmoothTime);
            transform.rotation = _currentRotation;
        }
    }
}
