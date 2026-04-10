using System;
using System.Collections.Generic;
using Game.Application.Asset;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Game.Application.GUI.OverlaySystem
{
    public class OverlaySystem : IInitializable, IOverlaySystem, IDisposable
    {
        [Inject] private IGUISystem _guiSystem;
        [Inject] private IAssetLoader _assetLoader;
        [Inject] private IObjectResolver _objectResolver;

        private Transform _overlaysParent;
        private readonly List<string> _createdAssetsKey = new();

        void IInitializable.Initialize()
        {
            _overlaysParent = _guiSystem.Canvas.CreateContainer("OverlaysGUI");
        }

        void IOverlaySystem.CreateOverlay(OverlayType type)
        {
            var overlayKey = type.ToString();
            var instance = _assetLoader.LoadImmediate<GameObject>(overlayKey);
            _objectResolver.Instantiate(instance, _overlaysParent);
            _createdAssetsKey.Add(overlayKey);
        }

        void IDisposable.Dispose()
        {
            foreach (var assetKey in _createdAssetsKey)
            {
                _assetLoader.Release(assetKey);
            }
            
            Object.Destroy(_overlaysParent.gameObject);
        }
    }
}