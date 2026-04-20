using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Game.Application.Asset
{
    public class SceneAssetHandle : ISceneAssetHandle
    {
        private readonly string _key;
        private AsyncOperationHandle<SceneInstance> _handle;

        string IAssetHandle.Key => _key;
        float IAssetHandle.Progress => _handle.IsValid() ? _handle.PercentComplete : 0f;
        AsyncOperationHandle<SceneInstance> ISceneAssetHandle.Handle => _handle;
        
        public SceneAssetHandle(string key)
        {
            _key = key;
        }

        UniTask<SceneInstance> ISceneAssetHandle.GetSceneAsync(LoadSceneMode loadMode, bool activateOnLoad)
        {
            if (_handle.IsValid())
            {
                return UniTask.FromResult(_handle.Result);
            }
            
            _handle = Addressables.LoadSceneAsync(_key, loadMode, activateOnLoad);
            return _handle.ToUniTask();
        }

        bool IAssetHandle.IsValid()
        {
            return _handle.IsValid();
        }

        void IAssetHandle.Release()
        {
            if (!_handle.IsValid()) return;
            _handle.Release();
            _handle = default;
        }
    }
}