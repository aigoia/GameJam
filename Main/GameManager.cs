using Script.Arcade;
using UnityEditor;
using UnityEngine;
using Cinemachine;

namespace Script.Main
{
    public class GameManager : MonoBehaviour
    {
        Camera arcadeCamera;
        CinemachineVirtualCamera arcadeVirtualCamera;
        Camera birdCamera;
        ArcadeController arcadeController;

        void Awake()
        {
            arcadeCamera = GameObject.Find("ArcadeCamera").transform.Find("MainCamera").GetComponent<Camera>();
            arcadeVirtualCamera = GameObject.Find("ArcadeCamera").GetComponent<CinemachineVirtualCamera>();
            birdCamera = GameObject.Find("BirdCamera").transform.Find("MainCamera").GetComponent<Camera>();
            arcadeController = FindAnyObjectByType<ArcadeController>();
        }

        void Start()
        {
            arcadeCamera.enabled = true;
            birdCamera.enabled = true;

            birdCamera.depth = 1;
            arcadeCamera.depth = 0;

            arcadeController.enabled = false;
            
        }

        void Update()
        {
            if (!Input.GetKeyDown(KeyCode.R)) return;

            if (birdCamera.depth > arcadeCamera.depth)
            {
                SwitchToArcade();
            }
            else
            {
                SwitchToBird();
            }
        }

        void SwitchToArcade()
        {
            var baseDirection = Vector3.one; 
            var variedDirection = new Vector3(
                baseDirection.x * ((Time.frameCount % 4 <= 0) ? -1 : 1),
                baseDirection.y * ((Time.frameCount % 4 <= 0) ? -1 : 1),
                baseDirection.z * ((Time.frameCount % 4 <= 0) ? -1 : -4)
            );
            
            arcadeVirtualCamera.ForceCameraPosition(
                arcadeController.transform.position + variedDirection, 
                arcadeController.transform.rotation
            );
            
            birdCamera.depth = 0;
            arcadeCamera.depth = 1;
            arcadeController.enabled = true;
        }

        void SwitchToBird()
        {
            birdCamera.depth = 1;
            arcadeCamera.depth = 0;
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
    }
}
