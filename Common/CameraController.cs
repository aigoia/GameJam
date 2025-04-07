using UnityEngine;

namespace Script.Common
{
    public class CameraController : MonoBehaviour
    {
        float MoveSpeed => 10f;  
        float SprintMultiplier => 2f;  // put shift key
        float MouseSensitivity => 100f;  

        float _rotationX = 0f;
        float _rotationY = 0f;

        void Update()
        {
            float speed = MoveSpeed * (Input.GetKey(KeyCode.LeftShift) ? SprintMultiplier : 1f);
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            transform.Translate(move * (speed * Time.deltaTime), Space.Self);

            if (!Input.GetMouseButton(1)) return; 
            
            float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

            _rotationX -= mouseY;
            _rotationY += mouseX;
            _rotationX = Mathf.Clamp(_rotationX, -90f, 90f); 

            transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0f);
        }
    }
}
