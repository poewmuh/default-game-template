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
        
        [SerializeField, Required] private GameObject _defaultSelectable;
        [SerializeField, Required] private CanvasGroup _canvasGroup;
        [SerializeField] private float _showDuration = 1f;
        [SerializeField] private float _hideDuration = .5f;
        
        protected IWindowSystem WindowSystem { get; private set; }
        
        void IWindow.Initialize(IWindowSystem windowSystem)
        {
            WindowSystem = windowSystem;
        }

        void IWindow.Show()
        {
            InternalShow(false);
        }

        void IWindow.Close()
        {
            InternalClose(false);
            OnClosed?.Invoke(this);
        }

        void IWindow.SetHidden(bool hidden)
        {
            if (hidden)
            {
                InternalClose(true);
            }
            else
            {
                InternalShow(true);
            }
        }
        
        protected virtual void InternalShow(bool isHidden)
        {
            gameObject.SetActive(true);
            
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(null);
            UniTask.WaitForEndOfFrame()
                .ContinueWith(() => eventSystem.SetSelectedGameObject(_defaultSelectable));
            
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0f;
            _canvasGroup.DOFade(1f, _showDuration).SetEase(Ease.InOutExpo).OnComplete(() =>
            {
                _canvasGroup.interactable = true;
            });
        }
        
        protected virtual void InternalClose(bool isHidden)
        {
            _canvasGroup.DOFade(0f, _hideDuration).SetEase(Ease.InOutExpo).OnComplete(() =>
            {
                _canvasGroup.interactable = false;
                gameObject.SetActive(false);
            });
        }
        
        protected virtual void OnDestroy()
        {
            OnClosed = null;
        }
        
        GameObject IWindow.GetDefaultSelectable() => _defaultSelectable;
    }
}