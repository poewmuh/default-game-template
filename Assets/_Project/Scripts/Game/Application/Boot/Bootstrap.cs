using Cysharp.Threading.Tasks;
using Game.Application.Scene;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Game.Application.Boot
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private IntroShower _introShower;
        
        [Inject] private ISceneSystem _sceneSystem;
        
        private void Start()
        {
            LoadAsync().Forget();
        }

        private async UniTaskVoid LoadAsync()
        {
            await _introShower.ShowIntroAsync();
            await _sceneSystem.LoadMainMenu();
        }
    }
}
