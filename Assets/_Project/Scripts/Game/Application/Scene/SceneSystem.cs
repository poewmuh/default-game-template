using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Application.Asset;
using Game.Application.GUI.LoadingSystem;
using Game.Application.Persistence;
using MessagePipe;
using R3;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;

namespace Game.Application.Scene
{
    public class SceneSystem : ISceneSystem
    {
        [Inject] private ScenesConfig _scenesConfig;
        [Inject] private IAssetLoader _assetLoader;
        [Inject] private IGameSaveMediator _gameSaveMediator;

        [Inject] private IPublisher<LoadingEventMessage> _loadingMessagePub;
        
        private readonly List<ISceneAssetHandle> _activeHandles = new();

        async UniTask ISceneSystem.LoadMainMenu()
        {
            UnloadAllScenes();
            await LoadAndTrack(_scenesConfig.MainMenuScene, LoadSceneMode.Single);
        }
        
        async UniTask ISceneSystem.LoadGameplay(int level)
        {
            _gameSaveMediator.GetLevelSaveData().SetLevelIndex(level);
            var data = _scenesConfig.GetGameplayScene(level);
            UnloadAllScenes();

            ReactiveProperty<bool> isLoaded = new();
            var additionalScenes = data.AdditionalSceneReference ?? new AssetReference[0];
            var totalScenes = additionalScenes.Length + 1;
            
            _loadingMessagePub.Publish(new LoadingEventMessage(() =>
            {
                var progress = _activeHandles.Sum(h => h.Progress);
                return isLoaded.CurrentValue ? 1f : progress / totalScenes;
            }));
            
            await LoadAndTrack(data.MainSceneReference, LoadSceneMode.Single);
        
            if (additionalScenes.Length > 0)
            {
                var tasks = additionalScenes
                    .Select(scene => LoadAndTrack(scene, LoadSceneMode.Additive));
            
                await UniTask.WhenAll(tasks);
            }

            _gameSaveMediator.Save();
            await UniTask.DelayFrame(1);
            isLoaded.Value = true;
        }
        
        async UniTask ISceneSystem.LoadScene(AssetReference scene, LoadSceneMode sceneMode)
        {
            if (sceneMode is LoadSceneMode.Single)
                UnloadAllScenes();

            await LoadAndTrack(scene, sceneMode);
        }

        void ISceneSystem.UnloadScene(string key)
        {
            var handle = _activeHandles.FirstOrDefault(h => h.Key == key);
            if (handle == null) return;
            
            _activeHandles.Remove(handle);
            _assetLoader.Release(key);
        }
        
        private async UniTask LoadAndTrack(AssetReference scene, LoadSceneMode mode)
        {
            var handle = _assetLoader.GetSceneHandle(scene.AssetGUID);
            _activeHandles.Add(handle);
            await handle.GetSceneAsync(mode);
        }

        private void UnloadAllScenes()
        {
            foreach (var handle in _activeHandles)
                _assetLoader.Release(handle.Key);

            _activeHandles.Clear();
        }
    }
}
