using UnityEngine;

namespace Script.Arcade
{
    public class PlayerCollider : MonoBehaviour
    {
        GameObject shieldMesh;
        int blockLayer;

        void Awake()
        {
            blockLayer = LayerMask.NameToLayer("Block");
            shieldMesh = transform.Find("Ball").Find("Shield").gameObject;
        }

        void OnCollisionEnter(Collision other)
        {
            shieldMesh.SetActive(other.gameObject.layer == blockLayer);
        }
    }
}
