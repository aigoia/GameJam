using Script.Common;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace Script.UI
{
    public class OperationButtonManager : MonoBehaviour
    {
        GameObject buttonPanel;
        Animator buttonAnimator;
        GameObject darker;
        public Image darkerImage; 
        Animator leftTopAnimator;
        Animator rightTopAnimator;
        Animator leftBottomAnimator;
        Animator rightBottomAnimator;
        Animator middlePanelAnimator;
        Button continueButton;
        Button exitButton;
        Button thisButton;
        Button bringOperationButton;
        CameraController cameraController;
        
        
        void Awake()
        {
            darker = GameObject.Find("Darker");
            darkerImage ??= darker.GetComponent<Image>();
            darkerImage.enabled = true;
            darker.SetActive(false);
            
            buttonPanel = GameObject.Find("ButtonPanel");
            buttonAnimator = buttonPanel.GetComponent<Animator>();
            
            leftTopAnimator = GameObject.Find("LeftTopPanel").GetComponent<Animator>();
            rightTopAnimator = GameObject.Find("RightTopPanel").GetComponent<Animator>();
            leftBottomAnimator = GameObject.Find("LeftBottomPanel").GetComponent<Animator>();
            rightBottomAnimator = GameObject.Find("RightBottomPanel").GetComponent<Animator>();
            middlePanelAnimator = GameObject.Find("MiddlePanel").GetComponent<Animator>();

            continueButton = GameObject.Find("Continue").GetComponent<Button>();
            thisButton = GetComponent<Button>();
            
            exitButton = GameObject.Find("Exit").GetComponent<Button>();
            thisButton = GetComponent<Button>();
            
            bringOperationButton =  GameObject.Find("BringOperation").GetComponent<Button>();
            bringOperationButton.onClick.AddListener(OnPlay);
            
            cameraController = FindAnyObjectByType<CameraController>();
        }

        void Start()
        {
            continueButton.onClick.AddListener(OnPlayContinue);
            exitButton.onClick.AddListener(QuitGame);
        }

        public void OnPlayContinue()
        {
            if (!darker.activeSelf) return;

            thisButton.onClick.Invoke();
            
            buttonAnimator.Play("Hide");
            
            leftTopAnimator.Play("Show");
            rightTopAnimator.Play("Show");
            leftBottomAnimator.Play("Show");
            rightBottomAnimator.Play("Show");
            middlePanelAnimator.Play("Show");
            
            cameraController.enabled = !cameraController.enabled;
            darker.SetActive(!darker.activeSelf);
            Cursor.visible = false;
        }
        
        public void OnPlay()
        {
            Cursor.visible = false;
            thisButton.onClick.Invoke();
            
            buttonAnimator.Play(darker.activeSelf ? "Hide" : "Show");
            
            leftTopAnimator.Play(darker.activeSelf ? "Show" : "Hide");
            rightTopAnimator.Play(darker.activeSelf ? "Show" : "Hide");
            leftBottomAnimator.Play(darker.activeSelf ? "Show" : "Hide");
            rightBottomAnimator.Play(darker.activeSelf ? "Show" : "Hide");
            middlePanelAnimator.Play(darker.activeSelf ? "Show" : "Hide");
            
            cameraController.enabled = !cameraController.enabled;
            darker.SetActive(!darker.activeSelf);
            Cursor.visible = darker.activeSelf;
        }
        
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
