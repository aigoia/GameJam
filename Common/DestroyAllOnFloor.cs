using UnityEngine;

namespace Script.Common
{
    public class DestroyAllOnFloor : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            foreach (GameObject @object in Object.FindObjectsOfType<GameObject>())
            {
                Destroy(@object);
            }
        }
    }
}
