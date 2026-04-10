using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Application.Scene
{
    [Serializable]
    public class GameplaySceneData
    {
        [field: SerializeField] public string DebugName { get; private set; }
        [field: SerializeField] public AssetReference MainSceneReference { get; private set; }
        [field: SerializeField] public AssetReference[] AdditionalSceneReference { get; private set; }
    }
}