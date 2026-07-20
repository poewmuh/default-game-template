using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TriInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Application.GUI.WindowSystem
{
    public class WindowBase : MonoBehaviour, IWindow
    {
        public event Action<WindowBase> OnClosed;
        public WindowState State { get; private set; } = WindowState.Closed;
        
        [SerializeField, Required] private GameObject _defaultSelectable;
        [SerializeField, Required] private CanvasGroup _canvasGroup;
        [SerializeField] private float _showDuration = 1f;
        [SerializeField] private float _hideDuration = .5f;
        
        private Tweener _fadeTween;
        protected IWindowSystem WindowSystem { get; private set; }
        
        void IWindow.Initialize(IWindowSystem windowSystem)
        {
            WindowSystem = windowSystem;
        }

        void IWindow.Show()
        {
            InternalShow();
        }

        void IWindow.Close()
        {
            if (State is WindowState.Closing or WindowState.Closed)
                return;

            InternalClose(false);
        }

        void IWindow.SetHidden(bool hidden)
        {
            if (hidden)
            {
                InternalClose(true);
            }
            else
            {
                InternalShow();
            }
        }
        
        protected virtual void InternalShow()
        {
            if (State is WindowState.Open or WindowState.Opening)
                return;

            State = WindowState.Opening;
            gameObject.SetActive(true);
            
            SelectDefaultNextFrame().Forget();
            
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0f;
            _fadeTween?.Kill();
            _fadeTween = _canvasGroup.DOFade(1f, _showDuration).SetEase(Ease.InOutExpo).OnComplete(() =>
            {
                State = WindowState.Open;
                _canvasGroup.interactable = true;
            });
        }
        
        protected virtual void InternalClose(bool isHidden)
        {
            if (State is WindowState.Closing or WindowState.Closed)
                return;

            State = isHidden ? WindowState.Hiding : WindowState.Closing;
            _canvasGroup.interactable = false;
            _fadeTween?.Kill();
            _fadeTween = _canvasGroup.DOFade(0f, _hideDuration).SetEase(Ease.InOutExpo).OnComplete(() =>
            {
                gameObject.SetActive(false);

                State = isHidden ? WindowState.Hidden : WindowState.Closed;
                if (!isHidden)
                {
                    OnClosed?.Invoke(this);
                }
            });
        }

        private async UniTaskVoid SelectDefaultNextFrame()
        {
            var eventSystem = EventSystem.current;
            if (eventSystem == null)
                return;

            eventSystem.SetSelectedGameObject(null);
            await UniTask.WaitForEndOfFrame(this);

            if (this == null || State is WindowState.Closing or WindowState.Closed)
                return;

            eventSystem.SetSelectedGameObject(_defaultSelectable);
        }
        
        protected virtual void OnDestroy()
        {
            _fadeTween?.Kill();
            OnClosed = null;
        }
        
        GameObject IWindow.GetDefaultSelectable() => _defaultSelectable;
    }
}