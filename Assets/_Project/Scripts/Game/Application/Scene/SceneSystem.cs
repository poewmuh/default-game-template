using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Application.Asset;
using Game.Application.GUI.LoadingSystem;
using MessagePipe;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;

namespace Game.Application.Scene
{
    public class SceneSystem : ISceneSystem
    {
        [Inject] private ScenesConfig _scenesConfig;
        [Inject] private IAssetLoader _assetLoader;

        [Inject] private IPublisher<LoadingEventMessage> _loadingMessagePub;
        
        private readonly List<string> _activeSceneKeys = new();

        async UniTask ISceneSystem.LoadMainMenu()
        {
            await LoadSingleScene(_scenesConfig.MainMenuScene);
        }
        
        async UniTask ISceneSystem.LoadGameplay(int level)
        {
            var data = _scenesConfig.GetGameplayScene(level);
            UnloadAllScenes();

            bool isLoaded = false;
            _loadingMessagePub.Publish(new LoadingEventMessage(() => isLoaded ? 1f : 0.5f));
            
            await LoadAndTrack(data.MainSceneReference, LoadSceneMode.Single);
        
            if (data.AdditionalSceneReference is { Length: > 0 })
            {
                var tasks = data.AdditionalSceneReference
                    .Select(scene => LoadAndTrack(scene, LoadSceneMode.Additive));
            
                await UniTask.WhenAll(tasks);
            }

            isLoaded = true;
        }
        
        async UniTask ISceneSystem.LoadScene(AssetReference scene, LoadSceneMode sceneMode)
        {
            if (sceneMode is LoadSceneMode.Single)
                UnloadAllScenes();

            await LoadAndTrack(scene, sceneMode);
        }

        void ISceneSystem.UnloadScene(string key)
        {
            if (!_activeSceneKeys.Remove(key)) return;
            
            _assetLoader.Release(key);
        }
        
        private async UniTask LoadSingleScene(AssetReference scene)
        {
            UnloadAllScenes();
            await LoadAndTrack(scene, LoadSceneMode.Single);
        }
        
        private async UniTask LoadAndTrack(AssetReference scene, LoadSceneMode mode)
        {
            await _assetLoader.LoadSceneAsync(scene, mode);
            _activeSceneKeys.Add(scene.AssetGUID);
        }

        private void UnloadAllScenes()
        {
            foreach (var key in _activeSceneKeys)
                _assetLoader.Release(key);

            _activeSceneKeys.Clear();
        }
    }
}
