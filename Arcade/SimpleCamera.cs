using UnityEngine;

namespace Script.Arcade
{
    public class SimpleCamera : MonoBehaviour
    {
        public Transform target;        
        
        Vector3 offset => new Vector3(0, 2f, -5f); 
        float followSpeedX => 5f;
        float followSpeedY => 5f;
        float followSpeedZ => 5f;
        
        float deadZoneWidth => 0.1f;
        float deadZoneHeight => 0.1f;
        float softZoneWidth => 0.3f;
        float softZoneHeight => 0.3f;

        Vector3 velocity = Vector3.zero;

        private void LateUpdate()
        {
            if (!target) return;
            
            Vector3 targetScreenPos = Camera.main.WorldToViewportPoint(target.position);
            
            Vector2 deadMin = new Vector2(0.5f - deadZoneWidth / 2f, 0.5f - deadZoneHeight / 2f);
            Vector2 deadMax = new Vector2(0.5f + deadZoneWidth / 2f, 0.5f + deadZoneHeight / 2f);

            Vector3 desiredPosition = target.position + offset;
            
            if (targetScreenPos.x < deadMin.x)
                desiredPosition.x -= (deadMin.x - targetScreenPos.x) * 5f;
            else if (targetScreenPos.x > deadMax.x)
                desiredPosition.x += (targetScreenPos.x - deadMax.x) * 5f;

            if (targetScreenPos.y < deadMin.y)
                desiredPosition.y -= (deadMin.y - targetScreenPos.y) * 5f;
            else if (targetScreenPos.y > deadMax.y)
                desiredPosition.y += (targetScreenPos.y - deadMax.y) * 5f;
            
            Vector3 newPos = transform.position;
            newPos.x = Mathf.Lerp(transform.position.x, desiredPosition.x, followSpeedX * Time.deltaTime);
            newPos.y = Mathf.Lerp(transform.position.y, desiredPosition.y, followSpeedY * Time.deltaTime);
            newPos.z = Mathf.Lerp(transform.position.z, desiredPosition.z, followSpeedZ * Time.deltaTime);
            transform.position = newPos;
            
            Vector3 lookTarget = target.position + Vector3.up * 1.5f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookTarget - transform.position), 5f * Time.deltaTime);
        }
    }
}