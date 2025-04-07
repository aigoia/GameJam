using System;
using System.Threading.Tasks;
using Script.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Hello
{
    public class SignAsync : MonoBehaviour
    {
        float ScaleFactor => 2f;
        float Speed => 2f;
        
        bool _isAnimating = true;
        Vector3 _initialScale;

        Button _button;
        
        void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }
        
        void OnClick()
        {
            _isAnimating = !_isAnimating;
            if (_isAnimating) _ = StartAnimation();
            // Show.Print($"isAnimating : {_isAnimating}");
        }

        void Start()
        {
            _ = StartAnimation();
        }
        
        async Task StartAnimation()
        {
            _initialScale = transform.localScale;
            float time = 0f;
            
            while (this != null && gameObject != null && isActiveAndEnabled)
            {
                time += Time.deltaTime * Speed;
                transform.localScale = _initialScale * (1 + (Mathf.Sin(time) * (ScaleFactor -1)));
                
                await Task.Yield();  
                
                if (_isAnimating == false) return;
            }
        }
    }
}
