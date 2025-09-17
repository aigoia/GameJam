using UnityEngine;

namespace Arcade
{
    public class PlayerCollider : MonoBehaviour
    {
        GameObject _shieldMesh;
        int _blockLayer;

        void Awake()
        {
            _blockLayer = LayerMask.NameToLayer("Block");
            _shieldMesh = transform.Find("Ball").Find("Shield").gameObject;
        }

        void OnCollisionEnter(Collision other)
        {
            _shieldMesh.SetActive(other.gameObject.layer == _blockLayer);
        }
    }
}
