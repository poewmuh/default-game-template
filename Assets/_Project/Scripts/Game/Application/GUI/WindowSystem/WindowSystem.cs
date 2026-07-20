using System;
using System.Collections.Generic;
using System.Linq;
using Game.Application.Asset;
using Game.Application.InputSystem;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Application.GUI.WindowSystem
{
    public class WindowSystem : IWindowSystem, IDisposable, IInitializable
    {
        [Inject] private IGUISystem _guiSystem;
        [Inject] private IObjectResolver _objectResolver;
        [Inject] private IAssetLoader _assetLoader;
        [Inject] private IInputSwitcher _inputSwitcher;
        
        private readonly List<IWindow> _windowList = new();
        private readonly Dictionary<IWindow, string> _windowAssetKeys = new();
        private readonly object _inputLock = new();
        private RectTransform _windowsContainer;
        private bool _isInputLocked;
        
        int IWindowSystem.WindowsCount => _windowList.Count;
        
        public Subject<IWindow> OnWindowOpened { get; } = new();
        public Subject<IWindow> OnWindowClosed { get; } = new();
        
        private readonly CompositeDisposable _disposables = new();

        void IInitializable.Initialize()
        {
            _windowsContainer = _guiSystem.Canvas.CreateContainer("Windows");
            
            OnWindowOpened.AddTo(_disposables);
            OnWindowClosed.AddTo(_disposables);
        }

        T IWindowSystem.Show<T>()
        {
            return Show<T>(typeof(T).Name);
        }

        T IWindowSystem.Show<T>(AssetSUID assetId)
        {
            return Show<T>(assetId != null ? assetId.UID : typeof(T).Name);
        }

        private T Show<T>(string key) where T : WindowBase
        {
            var instance = _assetLoader.LoadImmediate<GameObject>(key);
            IWindow window = _objectResolver.Instantiate(instance, _windowsContainer).GetComponent<IWindow>();
            window.Initialize(this);
            window.OnClosed += HandleOnWindowClosed;
            window.Show();
            _windowAssetKeys[window] = key;

            if (_windowList.Count > 0)
            {
                var lastWindow = _windowList.Last();
                lastWindow.SetHidden(true);
            }
            
            _windowList.Add(window);
            OnWindowOpened.OnNext(window);
            
            UpdateInputLock();
            return (T)window;
        }
        
        void IWindowSystem.Close(IWindow window) => Close(window);

        void IWindowSystem.SetHidden<T>(bool isHidden)
        {
            foreach (var window in _windowList)
            {
                if (window is T)
                {
                    window.SetHidden(isHidden);
                    return;
                }
            }
        }
        
        void IWindowSystem.Close<T>()
        {
            foreach (var window in _windowList)
            {
                if (window is T)
                {
                    Close(window);
                    return;
                }
            }
        }
        
        void IWindowSystem.CloseAll()
        {
            var windowsToClose = new List<IWindow>(_windowList);
            foreach (var window in windowsToClose)
            {
                window.Close();
            }
        }
        
        void IWindowSystem.CloseLast()
        {
            if (_windowList.Count <= 0) return;
            var lastWindow = _windowList.Last();
            Close(lastWindow);
        }

        bool IWindowSystem.IsShowing<T>()
        {
            foreach (var window in _windowList)
            {
                if (window is T)
                {
                    return true;
                }
            }

            return false;
        }

        private void Close(IWindow window)
        {
            if (!_windowList.Contains(window))
                return;

            window.Close();
        }

        private void HandleOnWindowClosed(WindowBase window)
        {
            window.OnClosed -= HandleOnWindowClosed;
            _windowList.Remove(window);
            if (!_windowAssetKeys.TryGetValue(window, out var assetKey))
            {
                assetKey = window.GetType().Name;
            }

            _windowAssetKeys.Remove(window);
            OnWindowClosed.OnNext(window);
            _assetLoader.Release(assetKey);
            UnityEngine.Object.Destroy(window.gameObject);

            if (_windowList.Count > 0)
            {
                var lastWindow = _windowList.Last();
                lastWindow.SetHidden(false);
            }

            UpdateInputLock();
        }

        void IDisposable.Dispose()
        {
            if (_isInputLocked)
                _inputSwitcher.ReleaseSwitch(_inputLock);

            _disposables.Dispose();
        }

        private void UpdateInputLock()
        {
            if (_windowList.Count > 0)
            {
                if (_isInputLocked)
                    return;

                _isInputLocked = true;
                _inputSwitcher.RequestSwitch(_inputLock, InputSwitchMode.UI);
                return;
            }

            if (!_isInputLocked)
                return;

            _isInputLocked = false;
            _inputSwitcher.ReleaseSwitch(_inputLock);
        }
    }
}