using System;
using UnityEngine;

namespace Script.Main
{
    public class UiManager : MonoBehaviour
    {
        public Canvas canvas;
        
        void Awake()
        {
            canvas = GetComponent<Canvas>();
        }
    }
}