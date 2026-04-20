using Game.Application.Persistence.Data;
using VContainer;

namespace Game.Application.Persistence
{
    public class GameSaveMediator : IGameSaveMediator
    {
        [Inject] private ISaveLoadSystem _saveLoadSystem;
        
        private GameSaveData gameSaveData;

        GameSaveData IGameSaveMediator.GameSaveData => _saveLoadSystem.CurrentSaveData;

        PlayerSaveData IGameSaveMediator.GetPlayerSaveData()
        {
            return _saveLoadSystem.CurrentSaveData.GameplaySaveData.PlayerSaveData;
        }

        LevelSaveData IGameSaveMediator.GetLevelSaveData()
        {
            return _saveLoadSystem.CurrentSaveData.GameplaySaveData.LevelSaveData;
        }

        void IGameSaveMediator.Save()
        {
            _saveLoadSystem.Save();
        }
    }
}