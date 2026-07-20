using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Game.Application.Asset
{
    public interface IAssetLoader : IDisposable
    {
        UniTask<T> LoadAssetAsync<T>(string key) where T : UnityEngine.Object;
        UniTask<T> LoadAssetAsync<T>(AssetSUID assetId) where T : UnityEngine.Object;
        UniTask<SceneInstance> LoadSceneAsync(AssetReference reference, LoadSceneMode mode,
            bool activateOnLoad);
        T LoadImmediate<T>(string key) where T : UnityEngine.Object;
        T LoadImmediate<T>(AssetSUID assetId) where T : UnityEngine.Object;
        void Release(string key);
        void Release(AssetSUID assetId);
        ISceneAssetHandle GetSceneHandle(string key);
    }
}
