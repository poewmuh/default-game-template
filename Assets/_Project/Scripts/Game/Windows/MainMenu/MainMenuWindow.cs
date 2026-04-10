using System;
using Game.Application.GUI.WindowSystem;
using Game.Application.Scene;
using R3;
using VContainer;

namespace Game.Windows.MainMenu
{
    public class MainMenuWindow : WindowBase
    {
        [Inject] private ISceneSystem _sceneSystem;
        
        private MainMenuWindowView _windowView;

        private void Awake()
        {
            TryGetComponent(out _windowView);
            
            _windowView.OnPlayButtonClicked.Subscribe(OnPlayClicked).AddTo(this);
            _windowView.OnQuitButtonClicked.Subscribe(OnQuitClicked).AddTo(this);
        }

        private void OnPlayClicked(Unit _)
        {
            WindowSystem.Close(this);
            _sceneSystem.LoadGameplay(0);
        }
        
        private void OnQuitClicked(Unit _)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else
			UnityEngine.Application.Quit();
#endif
        }
    }
}