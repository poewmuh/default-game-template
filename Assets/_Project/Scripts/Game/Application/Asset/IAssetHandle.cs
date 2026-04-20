using System;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Game.Application.Asset
{
    public interface IAssetHandle
    {
        string Key { get; }
        float Progress { get; }
        bool IsValid();
        void Release();
    }

    public interface ISimpleAssetHandle : IAssetHandle
    {
        T GetAsset<T>();
        UniTask<T> GetAssetAsync<T>();
    }
    
    public interface ISceneAssetHandle : IAssetHandle
    {
        AsyncOperationHandle<SceneInstance> Handle { get; }
        UniTask<SceneInstance> GetSceneAsync(LoadSceneMode loadMode, bool activateOnLoad = true);
    }
}