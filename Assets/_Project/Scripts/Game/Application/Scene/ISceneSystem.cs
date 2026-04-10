using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Game.Application.Scene
{
    public interface ISceneSystem
    {
        UniTask LoadMainMenu();
        UniTask LoadGameplay(int level);
        UniTask LoadScene(AssetReference scene, LoadSceneMode sceneMode);
        void UnloadScene(string key);
    }
}
