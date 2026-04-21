using DG.Tweening;
using Game.Application.GUI.WindowSystem;
using Game.Windows.MainMenu;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Application.EntryPoints
{
    public class MainMenuEntry : SceneEntryPoint
    {
        [SerializeField] private Transform _cameraTransform;
        
        [Inject] private IWindowSystem _windowSystem;

        protected override void Initialize()
        {
            base.Initialize();
            
            _windowSystem.Show<MainMenuWindow>();
            _cameraTransform.DOLocalRotate(Vector3.zero, 3f);
        }
    }
}