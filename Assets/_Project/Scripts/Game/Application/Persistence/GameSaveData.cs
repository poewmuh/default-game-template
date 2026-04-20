using System;
using Game.Application.Persistence.Data;
using Newtonsoft.Json;

namespace Game.Application.Persistence
{
    [Serializable]
    public class GameSaveData
    {
        [JsonProperty] public GameplaySaveData GameplaySaveData { get; set; }
    }
}