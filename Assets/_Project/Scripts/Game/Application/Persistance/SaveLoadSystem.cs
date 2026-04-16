using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using Utils;
using VContainer.Unity;

namespace Game.Application.Persistance
{
    public class SaveLoadSystem : ISaveLoadSystem, IInitializable
    {
        private FileSystem _fileSystem;
        
        void IInitializable.Initialize()
        {
            var subFolder = UnityEngine.Application.isEditor || Debug.isDebugBuild ? "Development" : "Production";
            _fileSystem = new FileSystem(subFolder);
        }
        
        bool ISaveLoadSystem.Save(object target, string path)
        {
            _fileSystem.SaveToFile(path, JsonConvert.SerializeObject(target));
            return _fileSystem.FileExists(path);
        }
        
        T ISaveLoadSystem.Load<T>(string path)
        {
            var text = _fileSystem.LoadTextFromFile(path);
            return JsonConvert.DeserializeObject<T>(text);
        }
        
        bool ISaveLoadSystem.Exists(string path)
        {
            return _fileSystem.FileExists(path);
        }
    }
}