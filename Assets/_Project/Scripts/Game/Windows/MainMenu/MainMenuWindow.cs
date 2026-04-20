using System;
using Game.Application.GUI.WindowSystem;
using Game.Application.Persistence;
using Game.Application.Scene;
using R3;
using VContainer;

namespace Game.Windows.MainMenu
{
    public class MainMenuWindow : WindowBase
    {
        [Inject] private ISceneSystem _sceneSystem;
        [Inject] private ISaveLoadSystem _saveLoadSystem;
        
        private MainMenuWindowView _windowView;

        private void Awake()
        {
            TryGetComponent(out _windowView);
            
            _windowView.OnPlayButtonClicked.Subscribe(OnPlayClicked).AddTo(this);
            _windowView.OnQuitButtonClicked.Subscribe(OnQuitClicked).AddTo(this);
        }

        private void OnPlayClicked(Unit _)
        {
            if (_saveLoadSystem.MetaData.Saves.Count > 0)
            {
                _saveLoadSystem.LoadSave(_saveLoadSystem.MetaData.Saves[0]);
            }
            else
            {
                _saveLoadSystem.CreateNewSave();
            }
            
            WindowSystem.Close(this);
            _sceneSystem.LoadGameplay(_saveLoadSystem.CurrentSaveData.GameplaySaveData.LevelSaveData.LevelIndex);
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