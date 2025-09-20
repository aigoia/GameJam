using System;
using Script.Common;
using UnityEditor;
using UnityEngine;

namespace Script.Arcade
{
    public class GameManager : MonoBehaviour
    {
        Camera arcadeCamera;
        Camera birdCamera;
        ArcadeController arcadeController;

        void Awake()
        {
            arcadeCamera = GameObject.Find("ArcadeCamera").transform.Find("MainCamera").gameObject.GetComponent<Camera>();
            birdCamera = GameObject.Find("BirdCamera").transform.Find("MainCamera").gameObject.GetComponent<Camera>();
            arcadeController = FindAnyObjectByType<ArcadeController>();
        }

        void Start()
        {
            arcadeCamera.enabled = false;
            arcadeController.enabled = false;
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

        void Update()
        {
            if (!Input.GetKeyDown(KeyCode.R)) return;
            arcadeCamera.enabled = !arcadeCamera.enabled;
            arcadeController.enabled = arcadeCamera.enabled;
        }
    }
}
