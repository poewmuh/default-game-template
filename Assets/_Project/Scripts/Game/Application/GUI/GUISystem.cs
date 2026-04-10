using System;
using Game.Application.Asset;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Game.Application.GUI
{
    public class GUISystem : IGUISystem, IInitializable, IDisposable
    {
        [Inject] private IObjectResolver _objectResolver;
        [Inject] private IAssetLoader _assetLoader;
        public CanvasBehaviour Canvas { get; private set; }

        void IInitializable.Initialize()
        {
            var instance = _assetLoader.LoadImmediate<GameObject>(nameof(CanvasBehaviour));
            Canvas = Object.Instantiate(instance).GetComponent<CanvasBehaviour>();
            Canvas.name = "GUI_Root";
            
            Object.DontDestroyOnLoad(Canvas);
        }

        void IDisposable.Dispose()
        {
            _assetLoader.Release(nameof(CanvasBehaviour));
        }
    }
}