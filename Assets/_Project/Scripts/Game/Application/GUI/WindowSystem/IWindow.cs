using System;
using UnityEngine;

namespace Game.Application.GUI.WindowSystem
{
    public enum WindowState
    {
        Closed = 0,
        Opening = 1,
        Open = 2,
        Hiding = 3,
        Hidden = 4,
        Closing = 5,
    }

    public interface IWindow
    {
        event Action<WindowBase> OnClosed;
        WindowState State { get; }
        
        void Initialize(IWindowSystem windowSystem);
        void Show();
        void Close();
        void SetHidden(bool hidden);
        GameObject GetDefaultSelectable();
    }
}