using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.Application.Asset
{
    public class SimpleAssetHandle : ISimpleAssetHandle
    {
        private readonly string _key;
        private AsyncOperationHandle _handle;

        string IAssetHandle.Key => _key;

        float IAssetHandle.Progress => _handle.IsValid() ? _handle.PercentComplete : 0f;

        public SimpleAssetHandle(string key)
        {
            _key = key;
        }

        bool IAssetHandle.IsValid()
        {
            return _handle.IsValid();
        }

        void IAssetHandle.Release()
        {
            if (!_handle.IsValid()) return;
            
            _handle.Release();
        }

        T ISimpleAssetHandle.GetAsset<T>()
        {
            if (_handle.IsValid())
            {
                return (T)_handle.Result;
            }
            
            var handle = Addressables.LoadAssetAsync<T>(_key);
            _handle = handle;
            return handle.WaitForCompletion();
        }

        UniTask<T> ISimpleAssetHandle.GetAssetAsync<T>()
        {
            if (_handle.IsValid())
            {
                return UniTask.FromResult((T)_handle.Result);
            }

            var handle = Addressables.LoadAssetAsync<T>(_key);
            _handle = handle;
            return handle.ToUniTask();
        }
    }
}