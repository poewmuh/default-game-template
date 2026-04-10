using System;
using Game.Application.GUI.WindowSystem;
using Game.Windows.Loading;
using MessagePipe;
using VContainer;
using VContainer.Unity;

namespace Game.Application.GUI.LoadingSystem
{
    public class LoadingSystem : IInitializable, IDisposable
    {
        [Inject] private IWindowSystem _windowSystem;
        [Inject] private ISubscriber<LoadingEventMessage> _loadingMessageSub;
        
        private IDisposable _subscription;
        
        void IInitializable.Initialize()
        {
            _subscription = _loadingMessageSub.Subscribe(OnLoading);
        }

        private void OnLoading(LoadingEventMessage msg)
        {
            var loadingWindow = _windowSystem.Show<LoadingWindow>();
            loadingWindow.Setup(msg.ProgressFunc);
        }

        void IDisposable.Dispose()
        {
            _subscription?.Dispose();
        }
    }
}