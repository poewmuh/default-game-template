using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Game.Application.Asset
{
    public class AddressablesLoader : IAssetLoader
    {
        private readonly Dictionary<string, IAssetHandle> _handles = new(256);
        
        UniTask<T> IAssetLoader.LoadAssetAsync<T>(string key)
        {
            return GetHandle(key).GetAssetAsync<T>();
        }
        
        UniTask<SceneInstance> IAssetLoader.LoadSceneAsync(AssetReference reference, LoadSceneMode mode, bool activateOnLoad)
        {
            return GetSceneHandle(reference.AssetGUID).GetSceneAsync(mode, activateOnLoad);
        }
        
        T IAssetLoader.LoadImmediate<T>(string key)
        {
            return GetHandle(key).GetAsset<T>();
        }
        
        public void Release(string key)
        {
            if (!_handles.TryGetValue(key, out var handle)) return;
            
            handle.Release();
            _handles.Remove(key);
        }

        ISceneAssetHandle IAssetLoader.GetSceneHandle(string key)
        {
            return GetSceneHandle(key);
        }

        void IDisposable.Dispose()
        {
            foreach (var handle in _handles.Values)
            {
                handle.Release();
            }
            
            _handles.Clear();
        }

        private ISimpleAssetHandle GetHandle(string key)
        {
            if (_handles.TryGetValue(key, out var handle))
            {
                if (handle is ISimpleAssetHandle simpleHandle)
                {
                    return simpleHandle;
                }
                
                Debug.LogError($"[AddressablesLoader] Wrong handle: {key}.");
            }

            var newHandle = new SimpleAssetHandle(key);
            _handles[key] = newHandle;
            return newHandle;
        }

        private ISceneAssetHandle GetSceneHandle(string key)
        {
            if (_handles.TryGetValue(key, out var handle))
            {
                if (handle is ISceneAssetHandle sceneHandle)
                {
                    return sceneHandle;
                }
                
                Debug.LogError($"[AddressablesLoader] Wrong handle: {key}.");
            }
            
            var newHandle = new SceneAssetHandle(key);
            _handles[key] = newHandle;
            return newHandle;
        }
    }
}
