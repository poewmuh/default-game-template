using Game.Application.Persistence.Data;

namespace Game.Application.Persistence
{
    public interface IGameSaveMediator
    {
        GameSaveData GameSaveData { get; }
        
        PlayerSaveData GetPlayerSaveData();
        LevelSaveData GetLevelSaveData();

        void Save();
    }
}