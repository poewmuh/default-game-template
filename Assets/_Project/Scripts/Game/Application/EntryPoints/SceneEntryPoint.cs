using System.Collections.Generic;
using Game.Application.GUI.OverlaySystem;
using UnityEngine;
using VContainer;

namespace Game.Application.EntryPoints
{
    public class SceneEntryPoint : MonoBehaviour
    {
        [SerializeField] private List<OverlayType> _usingOverlays;
        
        [Inject] private IOverlaySystem _overlaySystem;

        private void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            foreach (var overlayType in _usingOverlays)
            {
                _overlaySystem.CreateOverlay(overlayType);
            }
        }
    }
}