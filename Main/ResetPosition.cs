using UnityEngine;

namespace Script.Main
{
    public class ResetPosition : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            other.transform.position = Vector3.zero + Vector3.up;
        }
    }
}