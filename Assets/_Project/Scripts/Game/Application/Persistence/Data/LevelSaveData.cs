using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Application.Persistence.Data
{
    [Serializable]
    public class LevelSaveData
    {
        [JsonProperty]
        [field: SerializeField]
        public int LevelIndex { get; private set; }

        public void SetLevelIndex(int levelIndex)
        {
            LevelIndex = levelIndex;
        }
    }
}