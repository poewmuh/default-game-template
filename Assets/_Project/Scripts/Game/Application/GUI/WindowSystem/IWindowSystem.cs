using R3;

using Game.Application.Asset;

namespace Game.Application.GUI.WindowSystem
{
    public interface IWindowSystem
    {
        Subject<IWindow> OnWindowOpened { get; }
        Subject<IWindow> OnWindowClosed { get; }
        
        int WindowsCount { get; }
        
        T Show<T>() where T : WindowBase;
        T Show<T>(AssetSUID assetId) where T : WindowBase;
        void Close(IWindow window);
        void SetHidden<T>(bool isHidden) where T : IWindow;
        void Close<T>() where T : IWindow;
        void CloseAll();
        void CloseLast();
        bool IsShowing<T>() where T : IWindow;
    }
}