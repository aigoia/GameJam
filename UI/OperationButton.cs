using UnityEngine;
using UnityEngine.UIElements;

namespace Script.UI
{
    public class OperationButton : MonoBehaviour
    {
        public Transform Background => transform.Find("Background");
        public Animator Animator => GetComponent<Animator>();
        public Button Button => GetComponent<Button>();
    }
}
