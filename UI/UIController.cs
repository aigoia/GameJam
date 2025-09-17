using System.Collections.Generic;
using Script.Common;
using UnityEngine;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;

namespace Script.UI
{
    public class UIController : MonoBehaviour
    {
        GameObject _operationPanel; 
        OperationButtonManager _operationButtonManager;
        
        public List<Animator> buttonAnimatorList;
        int _selectedHash;
        int _normalHash;

        void Awake()
        {
            _selectedHash = Animator.StringToHash("Selected");
            _normalHash = Animator.StringToHash("Normal");
            _operationPanel = GameObject.Find("OperationPanel");
            _operationButtonManager = _operationPanel.GetComponent<OperationButtonManager>();
        }

        void Start()
        {
            Cursor.visible = false; 
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) _operationButtonManager.OnPlay();
        }
    }
}
