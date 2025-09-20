using System.Collections.Generic;
using Script.Common;
using UnityEngine;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;

namespace Script.UI
{
    public class UIController : MonoBehaviour
    {
        GameObject operationPanel; 
        OperationButtonManager operationButtonManager;
        
        public List<Animator> buttonAnimatorList;
        int selectedHash;
        int normalHash;

        void Awake()
        {
            selectedHash = Animator.StringToHash("Selected");
            normalHash = Animator.StringToHash("Normal");
            operationPanel = GameObject.Find("OperationPanel");
            operationButtonManager = operationPanel.GetComponent<OperationButtonManager>();
        }

        void Start()
        {
            Cursor.visible = false; 
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) operationButtonManager.OnPlay();
        }
    }
}
