using System;
using Game.Application.GUI.WindowSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Windows.Loading
{
    public class LoadingWindow : WindowBase
    {
        [SerializeField] private Image _progressBar;
        
        private bool _isLoading;
        private Func<float> _progressGetter;
        private float _currentProgress;
        
        public void Setup(Func<float> messageProgressFunc)
        {
            _progressGetter = messageProgressFunc;
            _isLoading = true;
        }
        
        private void Update()
        {
            if (!_isLoading)
                return;

            _progressBar.fillAmount = _currentProgress = _progressGetter.Invoke();

            if (_currentProgress >= 1f)
            {
                _isLoading = false;
                WindowSystem.Close(this);
            }
        }
    }
}