using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Arcade
{
    public class GameManager : MonoBehaviour
    {
        void Start()
        {

        }

        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
            Debug.Log("Game is exiting");
        }
    }
}
