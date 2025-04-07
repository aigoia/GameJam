using System;
using System.Threading.Tasks;
using Script.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Hello
{
    public class AsyncAwait : MonoBehaviour
    {
        Button _button;
        Vector3 _initialScale;
        bool _isAnimating = true;
        
        float ScaleFactor => 2f; 
        float Speed => 2f;
        
        void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(HelloWorld);
        }

        void Start()
        {
             _ = StartAnimation();   
        }

        void HelloWorld()
        {
            // Show.Print("Hello World");
            _isAnimating = !_isAnimating;
            if (_isAnimating) _ = StartAnimation();
        }

        
        async Task StartAnimation()
        {
            _initialScale = transform.localScale;
            float time = 0f;

            while (this != null && gameObject != null && isActiveAndEnabled)
            {
                time += Time.deltaTime * Speed;
                
                float scaleMultiplier = 1 + (Mathf.Sign(time) * (ScaleFactor - 1));
                transform.localScale = _initialScale * scaleMultiplier;

                await Task.Yield(); 
                if (_isAnimating == false) return;
            }
        }
    }
}
