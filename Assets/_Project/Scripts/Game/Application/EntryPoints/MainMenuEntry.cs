using Game.Application.GUI.WindowSystem;
using Game.Windows.MainMenu;
using VContainer;
using VContainer.Unity;

namespace Game.Application.EntryPoints
{
    public class MainMenuEntry : IStartable
    {
        [Inject] private IWindowSystem _windowSystem;
        
        void IStartable.Start()
        {
            _windowSystem.Show<MainMenuWindow>();
        }
    }
}