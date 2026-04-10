using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Game.Application.Asset
{
    public interface IAssetLoader : IDisposable
    {
        UniTask<T> LoadAsync<T>(string key) where T : UnityEngine.Object;
        UniTask<SceneInstance> LoadSceneAsync(AssetReference reference, LoadSceneMode mode);
        T LoadImmediate<T>(string key) where T : UnityEngine.Object;
        void Release(string key);
    }
}
