using Game.Application.Asset;
using Game.Application.Audio;
using Game.Application.GUI;
using Game.Application.GUI.LoadingSystem;
using Game.Application.GUI.OverlaySystem;
using Game.Application.GUI.WindowSystem;
using Game.Application.InputSystem;
using Game.Application.Persistence;
using Game.Application.Persistence.Data;
using Game.Application.Scene;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Application.LifetimeScopes
{
    public class SystemLifetimeScope : LifetimeScope
    {
        [SerializeField] private InitialGameSaveData _intialGameSaveData;
        [SerializeField] private ScenesConfig _scenesConfig;
        [SerializeField] private GameObject _audioSystem;
        [SerializeField] private InputSystem.InputSystem _inputSystem;
        [SerializeField] private Camera _camera;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterMessageBrokers(builder);
            RegisterCoreSystems(builder);
        }
        
        private void RegisterMessageBrokers(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe();
            builder.RegisterBuildCallback(x =>
                GlobalMessagePipe.SetProvider(x.AsServiceProvider()));
        }

        private void RegisterCoreSystems(IContainerBuilder builder)
        {
            builder.RegisterInstance(_intialGameSaveData);
            builder.RegisterInstance(_audioSystem.GetComponent<IAudioSystem>());
            builder.RegisterInstance(_camera);
            builder.RegisterInstance(_scenesConfig);
            builder.RegisterInstance(_inputSystem).AsImplementedInterfaces();

            builder.Register<GameSaveMediator>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SaveLoadSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AddressablesLoader>(Lifetime.Singleton).As<IAssetLoader>();
            builder.Register<SceneSystem>(Lifetime.Singleton).As<ISceneSystem>();
            builder.Register<GUISystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<WindowSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<InputSwitcher>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<LoadingSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<OverlaySystem>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
