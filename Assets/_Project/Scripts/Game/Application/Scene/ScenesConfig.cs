using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Application.Scene
{
    [CreateAssetMenu(fileName = nameof(ScenesConfig), menuName = "_Project/System/" + nameof(ScenesConfig))]
    public class ScenesConfig : ScriptableObject
    {
        [field: SerializeField] public AssetReference BootstrapScene { get; private set; }
        [field: SerializeField] public AssetReference MainMenuScene { get; private set; }
        
        [SerializeField] private GameplaySceneData[] _gameplayScenes;

        public GameplaySceneData GetGameplayScene(int level)
        {
            return _gameplayScenes[level % _gameplayScenes.Length];
        }
    }
}